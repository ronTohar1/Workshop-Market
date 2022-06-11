using MarketBackend.BusinessLayer.Buyers.Members;
using System;
using MarketBackend.BusinessLayer.Market.StoreManagment;
using System.Collections.Concurrent;
using MarketBackend.BusinessLayer.Buyers;
using MarketBackend.DataLayer.DataManagers;
using MarketBackend.DataLayer.DataDTOs.Market.StoreManagement;
using MarketBackend.DataLayer.DataDTOs;

namespace MarketBackend.BusinessLayer.Market; 
public class StoreController
{
	private IDictionary<int, Store> openStores;
	private IDictionary<int, Store> closedStores; 

	private MembersController membersController;

	private static int storeIdCounter = 0; // the next store id
	private static Mutex storeIdCounterMutex = new Mutex();

	private Mutex openStoresMutex; 
	private Mutex closedStoresMutex;

	private StoreDataManager storeDataManager; 

	// creates a new StoreController without stores yet
	public StoreController(MembersController membersController)
	{
		this.membersController = membersController;

		this.openStores = new ConcurrentDictionary<int, Store>(); 
		this.closedStores = new ConcurrentDictionary<int, Store>();

		this.openStoresMutex = new Mutex(); 
		this.closedStoresMutex = new Mutex();

		this.storeDataManager = StoreDataManager.GetInstance(); 
	}


	public Store? GetStore(int storeId)
    {
		Store s = GetOpenStore(storeId);
		if (s == null)
			s = GetClosedStore(storeId);
		return s;
    }

	public virtual Store? GetOpenStore(int storeId)
    {
		if (!openStores.ContainsKey(storeId))
			return null;
		return openStores[storeId];
    }

	public Store GetClosedStore(int storeId)
	{
		if (!closedStores.ContainsKey(storeId))
			return null;
		return closedStores[storeId];
	}

	// r 2.1
	// In our system a store's name is unique so it returns the storeId or throws an exception
	public int GetStoreIdByName(string storeName)
    {
		int id = openStores.FirstOrDefault(idStorePair => idStorePair.Value.GetName().Equals(storeName), new KeyValuePair<int, Store>(-1, null)).Key; 
		if (id == -1) // ids are >= 0 
			id = closedStores.FirstOrDefault(idStorePair => idStorePair.Value.GetName().Equals(storeName), new KeyValuePair<int, Store>(-1, null)).Key;

		if (id == -1)
        {
			throw new MarketException("A store with the name: " + storeName + " does not exists in the system");
		}

		return id;
	}

	// cc 5
	// r 3.2
	// opens a new store and returns its id
	public int OpenNewStore(int memberId, string storeName)
	{
		Member storeFounder = membersController.GetMember(memberId);
		if (storeFounder == null)
			throw new MarketException("The member id: " + memberId + " does not exists in the system");

		if (String.IsNullOrWhiteSpace(storeName))
			throw new MarketException("Store must have a name!");

		openStoresMutex.WaitOne();

		string errorDescription = CanAddOpenStore(storeName);
		if (errorDescription != null) {
			openStoresMutex.ReleaseMutex();
			throw new MarketException(errorDescription);
		}

		Store newStore = new Store(storeName, storeFounder, (memberId) => membersController.GetMember(memberId));
		DataStore dataStore = newStore.ToDataStore(); 


		DataStore dataStore = new DataStore()
		{
			Name = storeName,
			Founder = MemberDataManager.GetInstance().Find(storeFounder.Id),
			IsOpen = true,
			Products = new List<DataProduct>()
		//		public int Id { get; set; }
		//public string Name { get; set; }
		//public DataMember? Founder { get; set; }
		//public bool IsOpen { get; set; }
		//public IList<DataProduct> Products { get; set; }
		//public IList<DataPurchase> PurchaseHistory { get; set; }
		//public IList<DataStoreMemberRoles> MembersPermissions { get; set; }
		//public DataAppointmentsNode? Appointments { get; set; }

		//public DataStoreDiscountPolicyManager DiscountManager { get; set; }
		//public DataStorePurchasePolicyManager PurchaseManager { get; set; }

		//public IList<DataBid> Bids { get; set; }
		};
		storeDataManager.Add(dataStore);
		storeDataManager.Save();

		int newStoreId = dataStore.Id; 
		openStores.Add(newStoreId, );

		openStoresMutex.ReleaseMutex(); 

		return newStoreId; 

		// todo: try again the synchronization here, maybe need to synchronize that the member exists, when 
    }

	// returns null if can or an error message if not
	private string CanAddOpenStore(string storeName)
	{
		if (StoreExists(storeName)){
			return "A store with the name: " + storeName + " already exists"; 
        }
		return null; 
	}

	private static int GenerateStoreId()
    {
		storeIdCounterMutex.WaitOne();

		int result = storeIdCounter;
		storeIdCounter++;

		storeIdCounterMutex.ReleaseMutex(); 

		return result; 
    }

	// r 4.9
	public void CloseStore(int memberId, int storeId)
    {
		openStoresMutex.WaitOne(); 

		if (!openStores.ContainsKey(storeId))
        {
			openStoresMutex.ReleaseMutex();
			throw new MarketException("An open store with an id: " + storeId + " does not exist in the system");
		}

		Store store = openStores[storeId];

		closedStoresMutex.WaitOne();

		try // catching and throwing to release the mutexes
		{
			store.CloseStore(memberId); // the store checks permission so it needs to be at least a member
		}
		catch (Exception exception)
        {
			closedStoresMutex.ReleaseMutex();
			openStoresMutex.ReleaseMutex();
			
			throw exception; 
		}

		openStores.Remove(storeId);
		closedStores.Add(storeId, store);

		closedStoresMutex.ReleaseMutex(); 
		openStoresMutex.ReleaseMutex();
    }

	public virtual bool StoreExists(int storeId)
    {
		return openStores.ContainsKey(storeId) || closedStores.ContainsKey(storeId); 
    }

	public virtual bool StoreExists(string storeName)
	{
		try
		{
			int storeId = GetStoreIdByName(storeName);
			return StoreExists(storeId);
		}
        catch
        {
			return false; 
        }
	}

    // r 2.2, r 4.9
    // returns the search results from open stores by their ids
    public IDictionary<int, IList<Product>> SearchProductsInOpenStores(ProductsSearchFilter filter, bool storesWithProductsThatPassedFilter = true)
    {
        IEnumerable<KeyValuePair<int, Store>> passedStoresFilter = openStores.Where(pair => filter.FilterStore(pair.Value));

        IDictionary<int, IList<Product>> result = new Dictionary<int, IList<Product>>();

        IList<Product> passedProductsFilter;
        foreach (KeyValuePair<int, Store> idStorepair in passedStoresFilter)
        {
            passedProductsFilter = idStorepair.Value.SerachProducts(filter);
            if (!storesWithProductsThatPassedFilter || passedProductsFilter.Count > 0)
            {
                result[idStorepair.Key] = passedProductsFilter;
            }
        }
        return result;
    }

	// r 2.1, r 4.9
	public IList<int> SearchOpenStores(ProductsSearchFilter filter)
    {
		return openStores.Where(pair => filter.FilterStore(pair.Value)).Select(pair => pair.Key).ToList();
	}

	public bool HasRolesInMarket(int userId) {
		foreach (Store store in openStores.Values.Concat(closedStores.Values)) 
			if (PlaysRoleAtStore(store, userId))
				return true;
		return false;
	}
	private bool PlaysRoleAtStore(Store store, int memeberId)
		=> store.IsManager(memeberId) || store.IsCoOwner(memeberId) || store.IsFounder(memeberId);

	//for tests
    public StoreController()
    {

    }

	public virtual IDictionary<int, Store> GetOpenStores()
    {
		return openStores;
    }
}
