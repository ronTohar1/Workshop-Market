using System;
namespace MarketBackend.BusinessLayer.Market.StoreManagment
{ 
	public class Product
	{
		private string name;
		private int amountInInventory;
		private Dictionary<string, double> discounts; //mapping between discount code and discount percentage 
		private List<PurchaseOption> purchaseOptions;
		
		public Product(string product_name)
		{
			name = product_name;
			amountInInventory = 0;
			discounts = new Dictionary<string, double>();
			purchaseOptions = new List<PurchaseOption>();
		}
		public void AddNewDiscount(string discount_code, double discount_percentage) { 
			discounts[discount_code] = discount_percentage;
		}
		public void AddToInventory(int amountToAdd) { 
			amountInInventory = amountInInventory + amountToAdd;
		} 
		public void RemoveFromInventory(int amountToRemove) { 
			if (amountInInventory<amountToRemove)
				throw new StoreManagmentException($"Not enough products of {name} in storage");
			else
				amountInInventory = amountInInventory - amountToRemove;
		}
		public void AddPurchaseOption(PurchaseOption purchaseOption) {
			if (!purchaseOptions.Contains(purchaseOption))	
				purchaseOptions.Add(purchaseOption);
		}
		public void RemovePurchaseOption(PurchaseOption purchaseOption)
		{
			if (!purchaseOptions.Contains(purchaseOption))
				throw new StoreManagmentException($"Not enough products of {name} in storage");
			else
				purchaseOptions.Remove(purchaseOption);
		}

	}
}