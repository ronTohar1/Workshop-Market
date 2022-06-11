using System;
using System.Collections.Concurrent;
using MarketBackend.BusinessLayer.Buyers;
using MarketBackend.BusinessLayer.Buyers.Members;
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

        string? canBuyProductErrorMessage = store.CanBuyProduct(buyerId, productId, amount);
        if (canBuyProductErrorMessage != null)
        {
            throw new MarketException(canBuyProductErrorMessage);
        }

        // can add product to cart
        Buyer? b = buyersController.GetBuyer(buyerId);

        buyer.Cart.AddProductToCart(new ProductInBag(productId, storeId), amount, buyerId, b is Member);
    }

    // r 2,3
    public void RemoveProductFromCart(int buyerId, int storeId, int productId, int amount)
    {
        VerifyValidProductAmount(amount); // checking first for efficiency 
        Buyer buyer = GetBuyerOrThrowException(buyerId);
        Store store = GetOpenStoreOrThrowException(storeId);

        // maybe can remove product from cart (for example the cart checks the product exists etc. )
        Buyer? b = buyersController.GetBuyer(buyerId);
        buyer.Cart.RemoveProductFromCart(new ProductInBag(productId, storeId), buyerId, b is Member);
    }

    // cc 10
    // r I 3, r I 4
    // r 1.5
    public Purchase PurchaseCartContent(int buyerId, PaymentDetails paymentDetails, SupplyDetails supplyDetails)
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

        IDictionary<int, double> storesTotal = GetPurchaseTotal(shoppingBags);
        double purchaseTotal = storesTotal.Values.Sum(x => x); // Sum prices of all products

        int transactionId = externalServicesController.makePayment(paymentDetails);
        if (transactionId == -1)
        {
            TryRollback(storesTransactions);
            throw new MarketException("Could not make payment");
        }
        if (externalServicesController.makeDelivery(supplyDetails) == -1)
        {
            externalServicesController.CancelPayment(transactionId);
            TryRollback(storesTransactions);
            throw new MarketException("Could not send a delivery");
        }

        IDictionary<int, string> receipts = GetReceipt(storesTransactions,shoppingBags);

        ICollection<ShoppingBag> shoppingBagsInPurchase = new List<ShoppingBag>(shoppingBags); 
        UpdateBuyerAndStore(buyer, shoppingBags, storesTransactions);
        AddRecord(buyer, shoppingBagsInPurchase, storesTotal, receipts);

        string finalReceipt = String.Join("", receipts.Values);
        return new Purchase(buyer.Id, DateTime.Now, purchaseTotal, finalReceipt);
    }

    //an adaptation of the purchase cart for cases where its for a bid
    public Purchase PurchaseBid(Bid bid, int memberId, PaymentDetails paymentDetails, SupplyDetails supplyDetails)
    {
        if (bid.memberId != memberId)
            throw new MarketException("Not your bid to purchase");
        // Basic fields for the purcahse
        int buyerId = bid.memberId;
        int storeId = bid.storeId;
        int productId = bid.productId;
        double bidPrice = bid.bid;

        Store? s = storeController.GetStore(storeId);
        if (!s.CheckAllApproved(bid))
            throw new MarketException("Cant purchase bid until all owners and managers approve");

        Buyer buyer = this.GetBuyerOrThrowException(buyerId);

        IDictionary<int, int> storesTransactions;

        // get the product in a bag for the purchse 
        ProductInBag prod = new ProductInBag(productId, storeId);
        IDictionary<ProductInBag, int> products = new ConcurrentDictionary<ProductInBag, int>();
        products.Add(prod, 1);

        // set up in the theme of a normal purchase
        ShoppingBag bag = new ShoppingBag(bid.storeId, products);
        ICollection<ShoppingBag> shoppingBags = new List<ShoppingBag>();
        shoppingBags.Add(bag);

        VerifyStoresAndProducts(shoppingBags); //Verifying all stores are open and contain all products requested.
        string? cantBuy = ReserveProducts(buyerId, shoppingBags, out storesTransactions);
        if (cantBuy != null)
        {
            TryRollback(storesTransactions);
            throw new MarketException(cantBuy);
        }
        // ---------------------------------------------------------
        // Try buying products

        // setting up the price to be the bid price
        IDictionary<int, double> storesTotal = new ConcurrentDictionary<int, double>();
        storesTotal.Add(storeId, bidPrice);
        double purchaseTotal = storesTotal.Values.Sum(x => x); // Sum prices of all products

        int transactionId = externalServicesController.makePayment(paymentDetails);
        if (transactionId == -1)
        {
            TryRollback(storesTransactions);
            throw new MarketException("Could not make payment");
        }
        if (externalServicesController.makeDelivery(supplyDetails) == -1)
        {
            externalServicesController.CancelPayment(transactionId);
            TryRollback(storesTransactions);
            throw new MarketException("Could not send a delivery");
        }

        IDictionary<int, string> receipts = GetReceipt(storesTransactions, shoppingBags);

        ICollection<ShoppingBag> shoppingBagsInPurchase = new List<ShoppingBag>(shoppingBags);
        UpdateBuyerAndStore(buyer, shoppingBags, storesTransactions);
        AddRecord(buyer, shoppingBagsInPurchase, storesTotal, receipts);

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
            Store? store = storeController.GetStore(storeId);
            //Check if store is closed -----
            if (store == null)
                exceptions += $"Store with id: {storeId} does not exist\n";
            if (!IsOpenStore(storeId))
            {
                marketExceptions += $"Sorry, but {store.name} is closed for shopping!\n";
            }
            foreach (var productAmount in shoppingBag.ProductsAmounts)
            {
                int productId = productAmount.Key.ProductId;
                if (store.SearchProductByProductId(productId) == null)
                    exceptions += $"Product id {productId} does not exist in store: {store.name} (store id: {storeId})\n";
            }
            string? issue = store.purchaseManager.CanBuy(shoppingBag, store.GetName());
            if (issue != null)
                marketExceptions += issue;

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
                buyer.Cart.RemoveProductFromCart(productInBag, buyer.Id, buyer is Member);
            }
            store.notifyAllStoreOwners($"The buyer with the id:${buyer.Id} has purchased at the store: {store.name}");
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

            storeReceipt.Add(storeId, getReceipt(shoppingBag, store, transactionId));
        }
        return storeReceipt;
    }

    private string getReceipt(ShoppingBag shoppingBag, Store store, int transactionId)
    {
        string receipt = $" >> {store.name} purchase:\n";

        List<Product> products = store.GetTransactionProducts(transactionId);
        if (products == null)
            return "Couldnt get receipt";

        // Calculating product -> amount dictionary
        IDictionary<int, int> productsAmounts = shoppingBag.ProductsAmounts.ToDictionary(x => x.Key.ProductId, x => x.Value);

        foreach (Product product in products)
        {
            double prodPrice = product.GetPrice();
            int prodAmount = productsAmounts[product.id];

            double price = prodAmount * prodPrice;
            receipt += $" >> >> Product: {product.name}, Quantity: {prodAmount}, unit price: {product.GetPrice()},  total: {price} shekels \n";
        }

        (double total, double discount) = store.GetTotalBagCost(shoppingBag);
        receipt += $" >> >> {store.name} Total Before Discount: { total}, Total after discount: {total - discount}\n";
        return receipt;

    }

    //Assuming everything is valid
    private IDictionary<int, double> GetPurchaseTotal(ICollection<ShoppingBag> shoppingBags)
    {

        IDictionary<int, double> storesTotal = new Dictionary<int, double>();
        foreach (ShoppingBag shoppingBag in shoppingBags)
        {
            int storeId = shoppingBag.StoreId;
            Store store = storeController.GetStore(storeId);
            IDictionary<ProductInBag, int> products = shoppingBag.ProductsAmounts;

            (double total ,double discount) = store.GetTotalBagCost(shoppingBag);
            storesTotal.Add(storeId, total-discount);
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

    // for tests
    public void RemoveProductFromCartt(int buyerId, int storeId, int productId, int amount)
    {
        VerifyValidProductAmount(amount); // checking first for efficiency 
        Buyer buyer = GetBuyerOrThrowException(buyerId);
        Store store = GetOpenStoreOrThrowException(storeId);

        // maybe can remove product from cart (for example the cart checks the product exists etc. )
        Buyer? b = buyersController.GetBuyer(buyerId);
        buyer.Cart.RemoveProductFromCart(new ProductInBag(productId, storeId));
    }
}