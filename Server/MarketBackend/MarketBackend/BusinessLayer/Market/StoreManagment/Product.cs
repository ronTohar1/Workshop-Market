using System;
namespace MarketBackend.BusinessLayer.Market.StoreManagment
{ 
	public class Product
	{
		public string name { get; set; }
		public int amountInInventory { get; set; }
		public IList<PurchaseOption> purchaseOptions { get; }

		public IList<string> reviews;
		public double pricePerUnit { get; set; }

		private Mutex amountInInventoryMutex;
		private Mutex purchaseOptionsMutex;
		private Mutex reviewMutex;

		public Product(string product_name, double pricePerUnit)
		{
			this.name = product_name;
			this.amountInInventory = 0;
			this.purchaseOptions = new SynchronizedCollection<PurchaseOption>();
			this.reviews = new SynchronizedCollection<string>();
			this.pricePerUnit = pricePerUnit;

			amountInInventoryMutex = new Mutex();
			purchaseOptionsMutex = new Mutex();
			reviewMutex = new Mutex();
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
				throw new MarketException($"Not enough products of {name} in storage");
			}
			amountInInventory = amountInInventory - amountToRemove;
			amountInInventoryMutex.ReleaseMutex();
		}

		// r.4.2
		public void AddPurchaseOption(PurchaseOption purchaseOption) {
			if (!ContainsPurchasePolicy(purchaseOption))
			{
				purchaseOptionsMutex.WaitOne();
				purchaseOptions.Add(purchaseOption);
				purchaseOptionsMutex.ReleaseMutex();
			}
		}

		// r.4.2
		public void RemovePurchaseOption(PurchaseOption purchaseOption)
		{
			purchaseOptionsMutex.WaitOne();
			if (!ContainsPurchasePolicy(purchaseOption))
			{
				purchaseOptionsMutex.ReleaseMutex();
				throw new MarketException($"Not enough products of {name} in storage");
			}
			purchaseOptions.Remove(purchaseOption);
			purchaseOptionsMutex.ReleaseMutex();
		}
		// r.4.2
		public void AddProductReview(string memberRevierName,string review)
		{
			reviewMutex.WaitOne();
			reviews.Add(memberRevierName+": "+review);
			reviewMutex.ReleaseMutex();
		}

		public bool ContainsPurchasePolicy(PurchaseOption purchaseOption)
			=> purchaseOptions.Contains(purchaseOption);

   

	}
}