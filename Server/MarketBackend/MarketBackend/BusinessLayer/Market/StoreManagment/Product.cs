using MarketBackend.DataLayer.DataDTOs;
using MarketBackend.DataLayer.DataManagementObjects;
using MarketBackend.DataLayer.DataManagers;
using System;
using System.Collections.Concurrent;
namespace MarketBackend.BusinessLayer.Market.StoreManagment
{
	public class Product
	{
		public virtual int id { get; private set; }
		public virtual string name { get; private set; } // todo: is it okay to make it virtual for testing? 
		public virtual int amountInInventory { get; set; } // public set for testing 
		public IList<PurchaseOption> purchaseOptions { get; }
		public IDictionary<int,IList<string>> reviews; //mapping between member id and his reviews
		public double pricePerUnit { get; set; }
		public virtual string category { get; private set; }
		public double productdicount { get; set; }

		private static int storeIdCounter = 0; // the next store id
		private static Mutex storeIdCounterMutex = new Mutex();

		private Mutex amountInInventoryMutex;
		private Mutex purchaseOptionsMutex;
		private Mutex pricePerUnitMutex;
		private Mutex categoryMutex;
		private Mutex productDiscountMutex;

		public Mutex storeMutex { get; private set; }

		private ProductDataManager productDataManager; 

		public Product(string product_name, double pricePerUnit, string category, double productdicount = 0.0)
			: this(
				  GenerateProductId(), 
				  product_name, 
				  0,
				  new SynchronizedCollection<PurchaseOption>(),
				  new ConcurrentDictionary<int, IList<string>>(),
				  pricePerUnit,
				  category, 
				  productdicount
				  )
		{

		}

		private Product(int id, string product_name, int amountInInventory, 
			IList<PurchaseOption> purchaseOptions, IDictionary<int, IList<string>> reviews, 
			double pricePerUnit, string category, double productdicount = 0.0)
		{
			this.id = id;
			this.name = product_name;
			this.amountInInventory = amountInInventory;
			this.purchaseOptions = purchaseOptions;
			this.reviews = reviews; 
			this.pricePerUnit = pricePerUnit;
			this.category = category;
			this.productdicount = productdicount;

			amountInInventoryMutex = new Mutex();
			purchaseOptionsMutex = new Mutex();
			pricePerUnitMutex = new Mutex();
			categoryMutex = new Mutex();
			productDiscountMutex = new Mutex();

			storeMutex = new Mutex();

			this.productDataManager = ProductDataManager.GetInstance();

		}

		public Product(string product_name, double pricePerUnit, string category) : this(product_name, pricePerUnit, category, 0.0) { }

		// r S 8
		public static Product DataProductToProduct(DataProduct dataProduct)
        {
			IList<PurchaseOption> purchaseOptions = dataProduct.PurchaseOptions
				.Select(dataPO => dataPO.PurchaseOption).ToList();

			IDictionary<int, IList<string>> reviews = new ConcurrentDictionary<int, IList<string>>();
			int memberId; 
			foreach (DataProductReview dataReview in dataProduct.Reviews)
            {
				memberId = dataReview.Member.Id;
				if (!reviews.ContainsKey(memberId))
                {
					reviews[memberId] = new SynchronizedCollection<string>();

				}
				reviews[memberId].Append(dataReview.Review);
			}

			return new Product(dataProduct.Id, dataProduct.Name, dataProduct.AmountInInventory, 
				purchaseOptions, reviews, dataProduct.PricePerUnit, dataProduct.Category, 
				dataProduct.ProductDiscount);
		}

		public DataProduct ToNewDataProduct()
        {
			DataProduct result = new DataProduct()
			{
				// id is in the data layer 
				Name = name,
				AmountInInventory = amountInInventory,
				PricePerUnit = pricePerUnit,
				PurchaseOptions = purchaseOptions
					.Select(purchaseOption => PurchaseOptionToNewDataPurchaseOption(purchaseOption)).ToList(),
				Category = category,
				ProductDiscount = productdicount,
				Reviews = reviews.SelectMany(pair =>
					pair.Value.Select(review => ProductReviewToNewDataProductReview(review, pair.Key, null))).ToList()
			};

			foreach(DataProductReview dataProductReview in result.Reviews)
            {
				dataProductReview.Product = result; 
            }

			return result; 
	}

		private static DataPurchaseOption PurchaseOptionToNewDataPurchaseOption(PurchaseOption purchaseOption)
        {
			return new DataPurchaseOption()
			{
				PurchaseOption = purchaseOption
			}; 
        }

		private static DataProductReview ProductReviewToNewDataProductReview(string review, int memberId, DataProduct dataProduct)
		{
			return new DataProductReview()
			{
				Member = MemberDataManager.GetInstance().Find(memberId),
				Product = dataProduct,
				Review = review
			};
		}

		// r.4.1
		public virtual void AddToInventory(int amountToAdd, Action saveChanges)
		{
			amountInInventoryMutex.WaitOne();

			productDataManager.Update(id, product => product.AmountInInventory += amountToAdd);
			saveChanges();

			amountInInventory = amountInInventory + amountToAdd;

			amountInInventoryMutex.ReleaseMutex();
		}
		// cc 9
		// r.4.1
		public virtual void RemoveFromInventory(int amountToRemove, Action saveChanges)
		{
			amountInInventoryMutex.WaitOne();
			if (amountInInventory < amountToRemove)
			{
				amountInInventoryMutex.ReleaseMutex();
				throw new MarketException($"Not enough products of '{name}' in storage");
			}

			productDataManager.Update(id, product => product.AmountInInventory -= amountToRemove);
			saveChanges(); 

			amountInInventory = amountInInventory - amountToRemove;
			amountInInventoryMutex.ReleaseMutex();
		}

		// r.4.2
		public void AddPurchaseOption(PurchaseOption purchaseOption, Action saveChanges)
		{
			if (!ContainsPurchasePolicy(purchaseOption))
			{
				purchaseOptionsMutex.WaitOne();

				productDataManager.Update(id, product =>
					product.PurchaseOptions.Add(
						new DataPurchaseOption { PurchaseOption = purchaseOption })
					);
				saveChanges(); 

				purchaseOptions.Add(purchaseOption);
				purchaseOptionsMutex.ReleaseMutex();
			}
		}

		// r.4.2
		public void RemovePurchaseOption(PurchaseOption purchaseOption, Action saveChanges)
		{
			purchaseOptionsMutex.WaitOne();
			if (!ContainsPurchasePolicy(purchaseOption))
			{
				purchaseOptionsMutex.ReleaseMutex();
				throw new MarketException($"Not enough products of '{name}' in storage");
			}

			productDataManager.Update(id, product =>
			{
				foreach (DataPurchaseOption dataPurchaseOption in product.PurchaseOptions.Where(dataPO => dataPO.PurchaseOption == purchaseOption))
				{
					product.PurchaseOptions.Remove(dataPurchaseOption);
				}
			});

			saveChanges(); 

			purchaseOptions.Remove(purchaseOption);
			purchaseOptionsMutex.ReleaseMutex();
		}

		// r.4.2
		public void AddProductReview(int memberId, string review, Action saveChanges)
		{
			if (String.IsNullOrWhiteSpace(review))
				throw new MarketException($"can't recieve an empty comment");

			productDataManager.Update(id, product =>
				product.Reviews.Add(ProductReviewToNewDataProductReview(review, memberId, productDataManager.Find(id))));
			saveChanges(); 

			if (!reviews.ContainsKey(memberId))
				reviews[memberId] = new SynchronizedCollection<string>();
			reviews[memberId].Add(review);
		}
		// r.4.2
		public void SetProductCategory(string newCategory, Action saveChanges)
		{
			categoryMutex.WaitOne();

			productDataManager.Update(id, product => product.Category = newCategory);
			saveChanges(); 

			category = newCategory;

			categoryMutex.ReleaseMutex();
		}
		// r.4.2
		public void SetProductDiscountPercentage(double newDiscountPercentage, Action saveChanges)
		{
			productDiscountMutex.WaitOne();

			productDataManager.Update(id, product => product.ProductDiscount = newDiscountPercentage / 100);
			saveChanges();

			productdicount = newDiscountPercentage / 100;
			productDiscountMutex.ReleaseMutex();
		}
		// r.4.2
		public void SetProductPriceByUnit(double newPrice, Action saveChanges)
		{
			pricePerUnitMutex.WaitOne();

			productDataManager.Update(id, product => product.PricePerUnit = newPrice);
			saveChanges();

			pricePerUnit = newPrice;
			pricePerUnitMutex.ReleaseMutex();
		}

		public void RemoveData()
        {
			DataProduct product = productDataManager.Find(id);

			DataRemoveReviews(product); 
			DataRemovePurchaseOptions(product);

			productDataManager.Remove(id); 
        }

		private void DataRemoveReviews(DataProduct product)
        {
			foreach (DataProductReview dataProductReview in product.Reviews)
            {
				productDataManager.Remove(dataProductReview);
            }
        }

		private void DataRemovePurchaseOptions(DataProduct product)
		{
			foreach (DataPurchaseOption dataPurchaseOption in product.PurchaseOptions)
			{
				productDataManager.Remove(dataPurchaseOption);
			}
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

		public virtual double GetPrice()
			=> pricePerUnit * (1 - productdicount);

        public override int GetHashCode()
        {
            return this.id.GetHashCode();
        }

    }
}