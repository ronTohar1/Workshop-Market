using MarketBackend.BusinessLayer.Buyers.Members;
using MarketBackend.BusinessLayer.Market.StoreManagment;
using System.Collections.Concurrent;
using MarketBackend.BusinessLayer.Buyers;
using MarketBackend.DataLayer.DataManagers;
using MarketBackend.DataLayer.DataDTOs.Market.StoreManagement;

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
	public StoreController(MembersController membersController) : 
		this(membersController, new ConcurrentDictionary<int, Store>(), new ConcurrentDictionary<int, Store>())
	{

	}

	private StoreController(MembersController membersController, 
		IDictionary<int, Store> openStores, IDictionary<int, Store> closedStores)
	{
		this.membersController = membersController;

		this.openStores = openStores;
		this.closedStores = closedStores;

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
		DataStore dataStore = newStore.ToNewDataStore(); 
		storeDataManager.Add(dataStore);

		storeDataManager.Save();

		int newStoreId = dataStore.Id; 
		openStores.Add(newStoreId, newStore);

		openStoresMutex.ReleaseMutex(); 

		return newStoreId; 

		// todo: try again the synchronization here, maybe need to synchronize that the member exists, when 
    }


	// r S 8
	public static StoreController LoadStoreController(MembersController membersController)
	{
		// trying to load data 

		StoreDataManager storeDataManager = StoreDataManager.GetInstance();

		IList<DataStore> dataOpenStores = storeDataManager.Find(store => store.IsOpen);
		IList<DataStore> dataClosedStores = storeDataManager.Find(store => !store.IsOpen);

		Func<int, Member> membersGetter = memberId => membersController.GetMember(memberId);

		IDictionary<int, Store> openStores = DataStoresListToStoresDisctionary(dataOpenStores, membersGetter);
		IDictionary<int, Store> closedStores = DataStoresListToStoresDisctionary(dataClosedStores, membersGetter);

		return new StoreController(membersController, openStores, closedStores);
	}

	private static IDictionary<int, Store> DataStoresListToStoresDisctionary(IList<DataStore> dataStores, Func<int, Member> membersGetter)
	{
		IDictionary<int, Store> storesDictionary = new ConcurrentDictionary<int, Store>();
		foreach (DataStore dataStore in dataStores)
		{
			storesDictionary.Add(dataStore.Id, Store.DataStoreToStore(dataStore, membersGetter));
		}
		return storesDictionary;
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
			store.CloseStore(memberId, storeId); // the store checks permission so it needs to be at least a member
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
		=> store.IsManager(memeberId) || store.IsCoOwner(memeberId) || store.IsFounder(memeberId)
			|| store.IsThereVotingForCoOwnerAppointment(memeberId);


	//for tests
    public StoreController()
    {

    }

	public virtual IDictionary<int, Store> GetOpenStores()
    {
		return openStores;
    }

    public IList<Bid> GetAllMemberBids(int memberId)
    {
		IList<Bid> bids = new List<Bid>();
		foreach (Store openStore in openStores.Values)
			bids = bids.Concat(openStore.bids.Values.Where(bid => bid.memberId == memberId).ToList()).ToList();
		return bids;
	}
}
