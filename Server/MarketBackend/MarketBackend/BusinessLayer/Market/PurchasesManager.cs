using System;
using System.Collections.Concurrent;
using MarketBackend.BusinessLayer.Buyers;
using MarketBackend.BusinessLayer.Market.StoreManagment;

namespace MarketBackend.BusinessLayer.Market;
public class PurchasesManager
{
    private StoreController storeController;
    private BuyersController buyersController;
    private ExternalServicesController externalServicesController;
    private Mutex purchaseLock;

    // r and cc relevant generally to this class:
    // cc 7, cc 8

    public PurchasesManager(StoreController storeController, BuyersController buyersController, ExternalServicesController externalServicesController)
    {
        this.storeController = storeController;
        this.buyersController = buyersController;
        this.externalServicesController = externalServicesController;
        purchaseLock = new Mutex();
    }

    // r 2.3
    public void AddProductToCart(int buyerId, int storeId, int productId, int amount)
    {
        VerifyValidProductAmount(amount); // checking first for efficiency 
        Buyer buyer = GetBuyerOrThrowException(buyerId);
        Store store = GetOpenStoreOrThrowException(storeId);

        string canBuyProductErrorMessage = store.CanBuyProduct(buyerId, productId, amount);
        if (canBuyProductErrorMessage != null)
        {
            throw new MarketException(canBuyProductErrorMessage);
        }

        // can add product to cart

        buyer.Cart.AddProductToCart(new ProductInBag(productId, storeId), storeId);
    }

    // r 2,3
    public void RemoveProductFromCart(int buyerId, int storeId, int productId, int amount)
    {
        VerifyValidProductAmount(amount); // checking first for efficiency 
        Buyer buyer = GetBuyerOrThrowException(buyerId);
        Store store = GetOpenStoreOrThrowException(storeId);

        // maybe can remove product from cart (for example the cart checks the product exists etc. )

        buyer.Cart.RemoveProductFromCart(new ProductInBag(productId, storeId));
    }

    // cc 10
    // r I 3, r I 4
    // r 1.5
    public Purchase PurchaseCartContent(int buyerId)
    {
        Buyer buyer = this.GetBuyerOrThrowException(buyerId);
        Cart cart = buyer.Cart;
        lock (purchaseLock)
        {
            ICollection<ShoppingBag> shoppingBags = cart.ShoppingBags.Values;

            // Check if can buy all products in cart: __________________
            VerifyNotEmptyCart(cart);
            string? cantBuy = GetCantPurchaseString(buyerId, shoppingBags);
            if (cantBuy != null)
                throw new MarketException(cantBuy);
            // ---------------------------------------------------------

            // Try buying products
            IDictionary<int, double> storesTotal = GetPurchaseTotal(shoppingBags);
            double purchaseTotal = storesTotal.Values.Sum(x => x);

            if (!externalServicesController.makePayment())
                throw new MarketException("Could not make payment");
            if (!externalServicesController.makeDelivery())
                throw new MarketException("Could not make delivery");

            IDictionary<int, string> receipts = GetReceipt(shoppingBags);

            UpdateBuyerAndStore(buyer, shoppingBags);
            AddRecord(buyer, shoppingBags, storesTotal, receipts);

            string finalReceipt = String.Join("", receipts.Values);
            return new Purchase(buyer.Id, DateTime.Now, purchaseTotal, finalReceipt);


        }


    }

    //Adding record of purchase for buyer and store
    private void AddRecord(Buyer buyer, ICollection<ShoppingBag> shoppingBags, IDictionary<int, double> storesTotal, IDictionary<int, string> receipts)
    {
        double purchaseTotal = storesTotal.Values.Sum(x => x);
        string finalReceipt = String.Join("", receipts.Values);
        buyer.AddPurchase(new Purchase(buyer.Id, DateTime.Now, purchaseTotal, finalReceipt));

        //Adding record of the purchase from the stores
        foreach (ShoppingBag bag in shoppingBags)
        {
            Store store = storeController.GetStore(bag.StoreId);
            store.AddPurchaseRecord(store.founder.Id, new Purchase(buyer.Id, DateTime.Now, storesTotal[bag.StoreId], receipts[bag.StoreId]));
        }
    }

    private void UpdateBuyerAndStore(Buyer buyer, ICollection<ShoppingBag> shoppingBags)
    {
        foreach (ShoppingBag shoppingBag in shoppingBags)
        {
            Store store = storeController.GetStore(shoppingBag.StoreId);

            foreach (var prod in shoppingBag.ProductsAmounts)
            {
                ProductInBag productInBag = prod.Key;
                int amount = prod.Value;

                //Remove amount from store
                store.DecreaseProductAmountFromInventory(store.founder.Id, productInBag.ProductId, amount);

                //Remove from cart
                buyer.Cart.RemoveProductFromCart(productInBag);
            }
        }
    }


    private IDictionary<int, string> GetReceipt(ICollection<ShoppingBag> shoppingBags)
    {
        string receipt = "";
        IDictionary<int, string> storeReceipt = new Dictionary<int, string>();
        foreach (ShoppingBag shoppingBag in shoppingBags)
        {
            storeReceipt.Add(shoppingBag.StoreId, getReceipt(shoppingBag));
        }
        return storeReceipt;
    }

    private string getReceipt(ShoppingBag shoppingBag)
    {
        Store store = storeController.GetStore(shoppingBag.StoreId);
        string receipt = $" >> {store.name} purchase:\n";
        IDictionary<ProductInBag, int> products = shoppingBag.ProductsAmounts;
        double totalStorePrice = 0;

        foreach (var prod in products)
        {
            int productId = prod.Key.ProductId;
            int amount = prod.Value;
            Product product = store.SearchProductByProductId(productId);

            double price = amount * product.GetPrice();
            receipt += $" >> >> Product: {product.name}, Quantity: {amount}, unit price: {product.GetPrice()},  total: {price} shekels \n";

            totalStorePrice += price;
        }

        receipt += $" >> >> {store.name} Total: {totalStorePrice}\n";
        return receipt;

    }

    //Assuming everything is valid
    private IDictionary<int, double> GetPurchaseTotal(ICollection<ShoppingBag> shoppingBags)
    {
        IDictionary<int, double> storesTotal = new Dictionary<int, double>();
        foreach (ShoppingBag shoppingBag in shoppingBags)
        {
            Store store = storeController.GetStore(shoppingBag.StoreId);
            IDictionary<ProductInBag, int> products = shoppingBag.ProductsAmounts;
            storesTotal.Add(shoppingBag.StoreId, store.GetTotalBagCost(products.ToDictionary(x => x.Key.ProductId, x => x.Value)));
        }
        return storesTotal;
    }

    private string? GetCantPurchaseString(int buyerId, ICollection<ShoppingBag> shoppingBags)
    {
        string? cantPurhcaseDesc = null;
        foreach (ShoppingBag shoppingBag in shoppingBags)
        {
            int storeId = shoppingBag.StoreId;
            Store store = storeController.GetStore(storeId);
            //Check if store is open -----
            if (store == null)
                throw new ArgumentException($"Store with id: {storeId} does not exist");
            if (!IsOpenStore(storeId))
                cantPurhcaseDesc += $"Sorry, but {store.name} is closed for shopping!";
            // ----------------------------
            else
            {
                string cantBuy = GetCantPurchaseString(buyerId, shoppingBag);
                if (cantBuy != null)
                {
                    if (cantPurhcaseDesc == null)
                        cantPurhcaseDesc = $"Cant buy the following products in {store.name}:\n";
                    cantPurhcaseDesc += cantBuy;
                }
            }
        }
        return cantPurhcaseDesc;
    }

    // Assuming store is open and not null
    private string? GetCantPurchaseString(int buyerId, ShoppingBag shoppingBag)
    {
        Store store = storeController.GetStore(shoppingBag.StoreId);
        string? cantPurhcaseDesc = null;
        foreach (var item in shoppingBag.ProductsAmounts)
        {
            ProductInBag product = item.Key;
            int amount = item.Value;
            string? cantBuy = store.CanBuyProduct(buyerId, product.ProductId, amount);

            if (cantBuy != null)
                cantPurhcaseDesc += cantBuy;
        }
        return cantPurhcaseDesc;
    }



    private bool IsOpenStore(int storeId)
    {
        return storeController.GetOpenStore(storeId) != null;
    }

    private void VerifyNotEmptyCart(Cart cart)
    {
        if (cart == null)
            throw new Exception("Cart is null!");
        if (cart.isEmpty())
            throw new MarketException("Your cart is empty!");
    }

    private Buyer GetBuyerOrThrowException(int buyerId)
    {
        Buyer result = buyersController.GetBuyer(buyerId);
        if (result == null)
        {
            throw new ArgumentException("A buyer with id: " + buyerId + " does not exist in the system"); // not a market exception becuase the user does not enter the id
        }
        return result;
    }

    private Store GetOpenStoreOrThrowException(int storeId)
    {
        Store result = storeController.GetOpenStore(storeId);
        if (result == null)
        {
            throw new ArgumentException("The open store of id: " + storeId + " does not exist in the system"); // not a market exception becuase the user does not enter the id
        }
        return result;
    }

    // throws MarketException if amount is not valid
    private void VerifyValidProductAmount(int amount)
    {
        if (amount <= 0)
            throw new MarketException("The product amount has to be positive, given: " + amount);
    }
}