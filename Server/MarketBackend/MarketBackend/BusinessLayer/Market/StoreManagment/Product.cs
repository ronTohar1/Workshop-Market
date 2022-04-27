using System;
namespace MarketBackend.BusinessLayer.Market.StoreManagment
{ 
	public class Product
	{
		public string name { get; set; }
		public int amountInInventory { get; set; }
		public IList<PurchaseOption> purchaseOptions { get; }
		public double pricePerUnit { get; set; }

		private Mutex amountInInventoryMutex;
		private Mutex purchaseOptionsMutex;

		public Product(string product_name, double pricePerUnit)
		{
			this.name = product_name;
			this.amountInInventory = 0;
			this.purchaseOptions = new SynchronizedCollection<PurchaseOption>();
			this.pricePerUnit = pricePerUnit;

			amountInInventoryMutex = new Mutex();
		}
		// r.4.1
		public void AddToInventory(int amountToAdd) { 
			amountInInventoryMutex.WaitOne();
			amountInInventory = amountInInventory + amountToAdd;
			amountInInventoryMutex.ReleaseMutex();
		} 
		// cc 9
		// r.4.1
		public void RemoveFromInventory(int amountToRemove) { 
			amountInInventoryMutex.WaitOne();
			if (amountInInventory < amountToRemove) { 
				amountInInventoryMutex.ReleaseMutex();
				throw new StoreManagmentException($"Not enough products of {name} in storage");
			}
			amountInInventory = amountInInventory - amountToRemove;
			amountInInventoryMutex.ReleaseMutex();
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
			purchaseOptions.Remove(purchaseOption);
		}

	}
}