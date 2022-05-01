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
	public IDictionary<int, IList<Tuple<int, int>>> PurchaseCartContent(int buyerId, IDictionary<int, IList<Tuple<int,int>>> producstToBuyByStoresIds)
	{
		Buyer buyer = GetBuyerOrThrowException(buyerId);
		// Firstly check if can purchase the content
		bool canPurchase = true;
		IDictionary<int, IList<Tuple<int, int>>> productsCanNotPurchase = new ConcurrentDictionary<int, IList<Tuple<int, int>>>();
		foreach (int storeId in producstToBuyByStoresIds.Keys) {
			foreach (Tuple<int,int> productAmount in producstToBuyByStoresIds[storeId])
			{
				int productId = productAmount.Item1;
				int amount = productAmount.Item2;
				Store store = GetOpenStoreOrThrowException(storeId);
				if (store.CanBuyProduct(buyerId, productId, amount) != null) {
					// meaning that we couldn't purchase for that specific store 
					canPurchase = false;
					if (!productsCanNotPurchase.ContainsKey(storeId))
						canPurchase = false;
						productsCanNotPurchase.Add(storeId, new SynchronizedCollection<Tuple<int, int>>());
					productsCanNotPurchase[storeId].Add(productAmount);
				}
			}
		}
		// now update the cart and the store approprietly 
		if (canPurchase && externalServicesController.makePayment())
		{
			string purchaseDescriptionTitle = $"buyer with id {buyer.Id} has succefully purchased: \n";
			string purchaseAllAtoresDescription = purchaseDescriptionTitle;
			DateTime purchaseDate = DateTime.Now;
			double purchaseAllStoresTotal = 0;

			foreach (int storeId in producstToBuyByStoresIds.Keys) {
				
				string purchaseSingleStoreDescription = "";
				Store store = storeController.getStore(storeId);
				purchaseAllAtoresDescription = purchaseAllAtoresDescription + $"	from {storeController.getStore(storeId).name}:\n";
				
				foreach (Tuple<int, int> productAmount in producstToBuyByStoresIds[storeId]) {
					int productId = productAmount.Item1;
					int amount = productAmount.Item2;
					store.DecreaseProductAmountFromInventory(store.founder.Id, productId, amount);
					ProductInBag productInBag = buyer.Cart.GetProductInBag(storeId, productId);
					buyer.Cart.RemoveProductFromCart(productInBag);
					purchaseSingleStoreDescription = purchaseSingleStoreDescription + $"	> {amount} x {store.SearchProductByProductId(productId).name}  - {amount * store.SearchProductByProductId(productId).getUnitPriceWithDiscount()} shekels \n";
				}

				double purchaseSingleTotal = store.GetTotalBagCost(producstToBuyByStoresIds[storeId].ToDictionary(x=>x.Item1, x => x.Item2));
				store.AddPurchaseRecord(buyerId, purchaseDate, purchaseSingleTotal, purchaseDescriptionTitle + purchaseSingleStoreDescription+$"Total of: {purchaseSingleTotal} shekels\n");
				
				purchaseAllAtoresDescription = purchaseAllAtoresDescription + purchaseSingleStoreDescription;
				purchaseAllStoresTotal = purchaseAllStoresTotal + purchaseSingleTotal;
			}
			purchaseAllAtoresDescription = purchaseAllAtoresDescription + $"\n>>>Total of: {purchaseAllStoresTotal} shekels\n";
			buyer.AddPurchase(new Purchase(purchaseDate, purchaseAllStoresTotal, purchaseAllAtoresDescription));
			return null;
		}
		return productsCanNotPurchase;
	}

	private Buyer GetBuyerOrThrowException(int buyerId)
    {
		Buyer result = buyersController.GetBuyer(buyerId);
		if (result == null)
		{
			throw new ArgumentException("The buyer of id: " + buyerId + " does not exist in the system"); // not a market exception becuase the user does not enter the id
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
