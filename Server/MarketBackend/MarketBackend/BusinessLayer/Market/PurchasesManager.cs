using System;
using MarketBackend.BusinessLayer.Buyers;
namespace MarketBackend.BusinessLayer.Market;
public class PurchasesManager
{
	private StoreController storeController;
	private BuyersController buyersController;
	//private ExternalServicesController externalServicesController;   ?????? 

	public PurchasesManager(StoreController storeController, BuyersController buyersController)
	{
		this.storeController = storeController;
		this.buyersController = buyersController;
	}
	public void AddProductToCart(int userId, int storeId, int productId, int amount)
	{
	
	}
	public void RemoveProductFromCart(int userId, int storeId, int productId, int amount)
	{

	}
	public void PurchaseCartContent(int userId)
	{

	}
}
