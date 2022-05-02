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
    public purchaseAttempt PurchaseCartContent(int buyerId, IDictionary<int, IList<Tuple<int, int>>> productsByStoreId)
    {
        Buyer buyer = GetBuyerOrThrowException(buyerId);
        // Firstly check if can purchase the content
        bool canPurchase = true;
        string canNotPurchaseMessage = null;
        
        // Check if can purchase
        foreach (int storeId in productsByStoreId.Keys)
        {
            Store store = GetOpenStoreOrThrowException(storeId);
            string purchasable =  getNotPurchasable(store, productsByStoreId[storeId], buyerId);
            if (purchasable != null)
            {
                canPurchase = false;
                if (canNotPurchaseMessage==null)
                    canNotPurchaseMessage = purchasable;
                else
                    canNotPurchaseMessage = canNotPurchaseMessage + purchasable;
            }
        }


        // Update cart and store
        if (canPurchase)
        {
            if (externalServicesController.makePayment())
                return new purchaseAttempt("the payment attempt has failed, please address your local payment serviced");

            // Init descriptive strings
            string purchaseTitle = $"buyer with id {buyer.Id} has succefully purchased: \n";
            string purchaseDescription = purchaseTitle;
            double purchaseTotal = 0;
            DateTime purchaseDate = DateTime.Now;

            foreach (int storeId in productsByStoreId.Keys)
            {
                Store? store = storeController.GetStore(storeId);
                purchaseDescription += $"	from {store.name}:\n";
           
                // buy from the store and record the purhcase 
                string purchaseStoreDescription = null; //Holder for the description of the purchase

                double purchaseStoreTotal = BuyFromStore(store, storeId, productsByStoreId[storeId], buyer, out purchaseStoreDescription);
                store.AddPurchaseRecord(buyerId, purchaseDate, purchaseStoreTotal, purchaseTitle + purchaseStoreDescription + $"Total of: {purchaseStoreTotal} shekels\n");

                purchaseDescription += purchaseStoreDescription;
                purchaseTotal = purchaseTotal + purchaseStoreTotal;
            }
            purchaseDescription = purchaseDescription + $"\n>>>Total of: {purchaseTotal} shekels\n";
            Purchase purchase = new Purchase(purchaseDate, purchaseTotal, purchaseDescription)
            buyer.AddPurchase(purchase);
            return null;
        }
        return new purchaseAttempt(canNotPurchaseMessage);
    }


    private double BuyFromStore(Store store, int storeId, IList<Tuple<int, int>> products, Buyer buyer, out string purchaseDesc)
    {
        string description = "";
        foreach (Tuple<int, int> productAmount in products)
        {
            int productId = productAmount.Item1;
            int amount = productAmount.Item2;

            //Remove amount from store
            store.DecreaseProductAmountFromInventory(store.founder.Id, productId, amount);

            //Remove from cart
            ProductInBag productInBag = buyer.Cart.GetProductInBag(storeId, productId);
            buyer.Cart.RemoveProductFromCart(productInBag);

            description += $"	> {amount} x {store.SearchProductByProductId(productId).name}  - {amount * store.SearchProductByProductId(productId).getUnitPriceWithDiscount()} shekels \n";
        }
        double purchaseStoreTotal = store.GetTotalBagCost(products.ToDictionary(x => x.Item1, x => x.Item2));
        purchaseDesc = description;

        return purchaseStoreTotal;
    }

    private string getNotPurchasable(Store store, IList<Tuple<int, int>> products, int buyerId)
    {
        string output = null;
        foreach (Tuple<int, int> product in products)
        {
            int productId = product.Item1;
            int amount = product.Item2;
            string failedPurchaseMsg = store.CanBuyProduct(buyerId, productId, amount);

            if (failedPurchaseMsg != null)
            {   //Cannot purchase
                if (output==null)
                    output = failedPurchaseMsg;
                else
                    output = output+"\n"+failedPurchaseMsg;
            }
        }
        return output;
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