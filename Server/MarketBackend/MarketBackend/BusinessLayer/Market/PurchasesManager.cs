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

    // r and cc relevant generally to this class:
    // cc 7, cc 8

    public PurchasesManager(StoreController storeController, BuyersController buyersController, ExternalServicesController externalServicesController)
    {
        this.storeController = storeController;
        this.buyersController = buyersController;
        this.externalServicesController = externalServicesController;
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
        ICollection<ShoppingBag> shoppingBags = cart.ShoppingBags.Values;

        // Check if can buy all products in cart: __________________
        VerifyNotEmptyCart(cart);
        IDictionary<int, int> storesTransactions;

        VerifyStoresAndProducts(shoppingBags); //Verifying all stores are open and contain all products requested.
        string? cantBuy = ReserveProducts(buyerId, shoppingBags, out storesTransactions);
        if (cantBuy != null)
        {
            TryRollback(storesTransactions);
            throw new MarketException(cantBuy);
        }
        // ---------------------------------------------------------
        // Try buying products

        IDictionary<int, double> storesTotal = GetPurchaseTotal(storesTransactions, shoppingBags);
        double purchaseTotal = storesTotal.Values.Sum(x => x); // Sum prices of all products

        if (!externalServicesController.makePayment())
        {
            TryRollback(storesTransactions);
            throw new MarketException("Could not make payment");
        }
        if (!externalServicesController.makeDelivery())
        {
            externalServicesController.CancelPayment();
            TryRollback(storesTransactions);
            throw new MarketException("Could not send a delivery");
        }

        IDictionary<int, string> receipts = GetReceipt(storesTransactions, shoppingBags);

        UpdateBuyerAndStore(buyer, shoppingBags, storesTransactions);
        AddRecord(buyer, shoppingBags, storesTotal, receipts);

        string finalReceipt = String.Join("", receipts.Values);
        return new Purchase(buyer.Id, DateTime.Now, purchaseTotal, finalReceipt);




    }

    // Verifying all stores are open and the items in the shopping bag exist in the store.
    // Throwing an exception if needed.
    private void VerifyStoresAndProducts(ICollection<ShoppingBag> shoppingBags)
    {
        string exceptions = null;
        string marketExceptions = null;
        foreach (ShoppingBag shoppingBag in shoppingBags)
        {
            int storeId = shoppingBag.StoreId;
            Store store = storeController.GetStore(storeId);
            //Check if store is closed -----
            if (store == null)
                exceptions += $"Store with id: {storeId} does not exist\n";
            if (!IsOpenStore(storeId))
            {
                marketExceptions += $"Sorry, but {store.name} is closed for shopping!\n";

                foreach (var productAmount in shoppingBag.ProductsAmounts)
                {
                    int productId = productAmount.Key.ProductId;
                    if (store.SearchProductByProductId(productId) == null)
                        exceptions += $"Product id {productId} does not exist in store: {store.name} (store id: {storeId})\n";
                }
            }
        }
        if (exceptions != null)
            throw new Exception(exceptions);
        if (marketExceptions != null)
            throw new MarketException(marketExceptions);
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

    private void UpdateBuyerAndStore(Buyer buyer, ICollection<ShoppingBag> shoppingBags, IDictionary<int, int> storesTransactions)
    {
        foreach (ShoppingBag shoppingBag in shoppingBags)
        {
            int storeId = shoppingBag.StoreId;

            Store store = storeController.GetStore(storeId);
            store.CommitTransaction(storesTransactions[storeId]);

            foreach (var prod in shoppingBag.ProductsAmounts)
            {
                ProductInBag productInBag = prod.Key;
                int amount = prod.Value;

                //Remove from cart
                buyer.Cart.RemoveProductFromCart(productInBag);
            }
        }
    }


    private IDictionary<int, string> GetReceipt(IDictionary<int, int> storesTransactions, ICollection<ShoppingBag> shoppingBags)
    {
        string receipt = "";
        IDictionary<int, string> storeReceipt = new Dictionary<int, string>();
        foreach (ShoppingBag shoppingBag in shoppingBags)
        {
            int storeId = shoppingBag.StoreId;
            int transactionId = storesTransactions[storeId];
            Store store = storeController.GetStore(storeId);

            IDictionary<int, int> productAmount = shoppingBag.ProductsAmounts.ToDictionary(x => x.Key.ProductId, x => x.Value);
            storeReceipt.Add(storeId, getReceipt(productAmount, store, transactionId));
        }
        return storeReceipt;
    }

    private string getReceipt(IDictionary<int, int> productsAmounts, Store store, int transactionId)
    {
        string receipt = $" >> {store.name} purchase:\n";
        double totalStorePrice = 0;

        List<Product> productsPrices = store.GetTransactionProducts(transactionId);
        if (productsPrices == null)
            return "Couldnt get receipt";

        foreach (Product product in productsPrices)
        {
            double prodPrice = product.GetPrice();
            int prodAmount = productsAmounts[product.id];

            double price = prodAmount * prodPrice;
            receipt += $" >> >> Product: {product.name}, Quantity: {prodAmount}, unit price: {product.GetPrice()},  total: {price} shekels \n";

            totalStorePrice += price;
        }

        receipt += $" >> >> {store.name} Total: {totalStorePrice}\n";
        return receipt;

    }

    //Assuming everything is valid
    private IDictionary<int, double> GetPurchaseTotal(IDictionary<int, int> storesTransactions, ICollection<ShoppingBag> shoppingBags)
    {

        IDictionary<int, double> storesTotal = new Dictionary<int, double>();
        foreach (ShoppingBag shoppingBag in shoppingBags)
        {
            int storeId = shoppingBag.StoreId;
            Store store = storeController.GetStore(storeId);
            IDictionary<ProductInBag, int> products = shoppingBag.ProductsAmounts;
            int transactionId = storesTransactions[storeId];
            storesTotal.Add(storeId, store.GetTransactionProducts(transactionId).Sum(x => x.GetPrice()));
        }
        return storesTotal;
    }

    private void TryRollback(IDictionary<int, int> transactions)
    {
        List<int> failedRollbacks = RollbackTransactions(transactions);
        if (failedRollbacks.Count != 0)
            throw new Exception("Couldn't rollback following stores:" + failedRollbacks.ToString());
    }

    private List<int> RollbackTransactions(IDictionary<int, int> transactions)
    {
        List<int> failedRollbacks = new();
        foreach (var transaction in transactions)
        {
            int storeId = transaction.Key;
            int transId = transaction.Value;
            Store store = storeController.GetStore(storeId);
            bool success = store.RollbackTransaction(transId);
            if (!success)
                failedRollbacks.Add(storeId);
        }
        return failedRollbacks;
    }

    // Reserve products at each store.
    // If succeeded, the <storeId, TransactionID> pairs will be put in the "transactions" variable.
    // If failed, a failure string will be returned, else null
    private string? ReserveProducts(int buyerId, ICollection<ShoppingBag> shoppingBags, out IDictionary<int, int> transactions)
    {
        string? cantPurhcaseDesc = null;
        IDictionary<int, int> validTransactions = new Dictionary<int, int>();
        foreach (ShoppingBag shoppingBag in shoppingBags)
        {
            int storeId = shoppingBag.StoreId;
            Store store = storeController.GetStore(storeId);
            //Check if store is closed -----
            if (store == null)
                throw new ArgumentException($"Store with id: {storeId} does not exist");
            if (!IsOpenStore(storeId))
                cantPurhcaseDesc += $"Sorry, but {store.name} is closed for shopping!";
            // ----------------------------
            else
            {
                int transactionId;
                string? storeCantBuy = ReserveStoreProducts(buyerId, store, shoppingBag, out transactionId);
                if (storeCantBuy != null)
                    cantPurhcaseDesc += $"Cant buy the following products in {store.name}:\n" + storeCantBuy;
                else
                    validTransactions.Add(storeId, transactionId);
            }
        }
        transactions = validTransactions;
        return cantPurhcaseDesc;
    }


    // Assuming store is open and not null
    // Reserving the products of the shopping bag and assigns the transaction id.
    // If failed, an informative string is returned, else null.
    private string? ReserveStoreProducts(int buyerId, Store store, ShoppingBag shoppingBag, out int transactionId)
    {
        IDictionary<int, int> products = shoppingBag.ProductsAmounts.ToDictionary(x => x.Key.ProductId, y => y.Value);
        string? cantPurhcaseDesc = store.ReserveProducts(buyerId, products, out transactionId);
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