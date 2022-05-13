using System;
using System.Collections.Concurrent;
namespace MarketBackend.BusinessLayer.Market.StoreManagment
{
	public class Product
	{
		public int id { get; private set; }
		public virtual string name { get; set; } // todo: is it okay to make it virtual for testing? 
		public virtual int amountInInventory { get; set; }
		public IList<PurchaseOption> purchaseOptions { get; }
		public IList<string> reviews; //mapping between member name and 
		public double pricePerUnit { get; set; }
		public virtual string category { get; private set; }
		public double productdicount { get; set; }

		private static int storeIdCounter = 0; // the next store id
		private static Mutex storeIdCounterMutex = new Mutex();

		private Mutex amountInInventoryMutex;
		private Mutex purchaseOptionsMutex;
		private Mutex reviewMutex;
		private Mutex pricePerUnitMutex;
		private Mutex categoryMutex;
		private Mutex productDiscountMutex;

		public Product(string product_name, double pricePerUnit, string category, double productdicount = 0.0)
		{
			this.id = GenerateProductId();
			this.name = product_name;
			this.amountInInventory = 0;
			this.purchaseOptions = new SynchronizedCollection<PurchaseOption>();
			this.reviews = new SynchronizedCollection<string>(); // 
			this.pricePerUnit = pricePerUnit;
			this.category = category;
			this.productdicount = productdicount;

			amountInInventoryMutex = new Mutex();
			purchaseOptionsMutex = new Mutex();
			reviewMutex = new Mutex();
			pricePerUnitMutex = new Mutex();
			categoryMutex = new Mutex();
			productDiscountMutex = new Mutex();

		}

		public Product(string product_name, double pricePerUnit, string category) : this(product_name, pricePerUnit, category, 0.0) { }

		// r.4.1
		public void AddToInventory(int amountToAdd)
		{
			amountInInventoryMutex.WaitOne();
			amountInInventory = amountInInventory + amountToAdd;
			amountInInventoryMutex.ReleaseMutex();
		}
		// cc 9
		// r.4.1
		public void RemoveFromInventory(int amountToRemove)
		{
			amountInInventoryMutex.WaitOne();
			if (amountInInventory < amountToRemove)
			{
				amountInInventoryMutex.ReleaseMutex();
				throw new MarketException($"Not enough products of {name} in storage");
			}
			amountInInventory = amountInInventory - amountToRemove;
			amountInInventoryMutex.ReleaseMutex();
		}

		// r.4.2
		public void AddPurchaseOption(PurchaseOption purchaseOption)
		{
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
		public void AddProductReview(string memberRevierName, string review)
		{
			reviewMutex.WaitOne();
			reviews.Add(memberRevierName + ": " + review);
			if (String.IsNullOrWhiteSpace(review)) {
				reviewMutex.ReleaseMutex();
				throw new MarketException($"can't recieve an empty comment");
			}

			reviewMutex.ReleaseMutex();
		}
		// r.4.2
		public void SetProductCategory(string newCategory)
		{
			categoryMutex.WaitOne();
			category = newCategory;
			categoryMutex.ReleaseMutex();
		}
		// r.4.2
		public void SetProductDiscountPercentage(double newDiscountPercentage)
		{
			productDiscountMutex.WaitOne();
			productdicount = newDiscountPercentage / 100;
			productDiscountMutex.ReleaseMutex();
		}
		// r.4.2
		public void SetProductPriceByUnit(double newPrice)
		{
			pricePerUnitMutex.WaitOne();
			pricePerUnit = newPrice;
			pricePerUnitMutex.ReleaseMutex();
		}
		private static int GenerateProductId()
		{
			storeIdCounterMutex.WaitOne();

			int result = storeIdCounter;
			storeIdCounter++;

			storeIdCounterMutex.ReleaseMutex();

			return result;
		}

		public bool ContainsPurchasePolicy(PurchaseOption purchaseOption)
			=> purchaseOptions.Contains(purchaseOption);

		public double GetPrice()
			=> pricePerUnit * (1 - productdicount);

        public override int GetHashCode()
        {
            return this.id.GetHashCode();
        }

    }
}