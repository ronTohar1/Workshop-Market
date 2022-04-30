using System;
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
	public IDictionary<int, IList<int>> PurchaseCartContent(int buyerId, IDictionary<int, IList<int>> producstToBuyByStoresIds)
	{
		throw new Exception(); 
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
