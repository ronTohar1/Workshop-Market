using System;
namespace MarketBackend.BusinessLayer.Market.StoreManagment
{ 
	public class Product
	{
		public string name { get; set; }
		public int amountInInventory { get; set; }
		public List<PurchaseOption> purchaseOptions { get; }

		public double pricePerUnit { get; set; }

		public Product(string product_name, double pricePerUnit)
		{
			this.name = product_name;
			this.amountInInventory = 0;
			this.purchaseOptions = new List<PurchaseOption>();
			this.pricePerUnit = pricePerUnit;
		}
		// r.4.1
		public void AddToInventory(int amountToAdd) { 
			amountInInventory = amountInInventory + amountToAdd;
		} 
		// cc 9
		// r.4.1
		public void RemoveFromInventory(int amountToRemove) { 
			if (amountInInventory<amountToRemove)
				throw new StoreManagmentException($"Not enough products of {name} in storage");
			else
				amountInInventory = amountInInventory - amountToRemove;
		}

		// r.4.2
		public void AddPurchaseOption(PurchaseOption purchaseOption) {
			if (!purchaseOptions.Contains(purchaseOption))	
				purchaseOptions.Add(purchaseOption);
		}

		// r.4.2
		public void RemovePurchaseOption(PurchaseOption purchaseOption)
		{
			if (!purchaseOptions.Contains(purchaseOption))
				throw new StoreManagmentException($"Not enough products of {name} in storage");
			else
				purchaseOptions.Remove(purchaseOption);
		}

	}
}