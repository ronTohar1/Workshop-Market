using MarketBackend.BusinessLayer.Buyers.Members;
using System;
using MarketBackend.BusinessLayer.Market.StoreManagment;
using System.Collections.Concurrent;

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

	// creates a new StoreController without stores yet
	public StoreController(MembersController membersController)
	{
		this.membersController = membersController;

		this.openStores = new ConcurrentDictionary<int, Store>(); 
		this.closedStores = new ConcurrentDictionary<int, Store>();

		this.openStoresMutex = new Mutex(); 
		this.closedStoresMutex = new Mutex();
	}


	public Store getStore(int storeId)
    {
		Store s = GetOpenStore(storeId);
		if (s == null)
			s = GetClosedStore(storeId);
		return s;
    }

	public Store GetOpenStore(int storeId)
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
			throw new ArgumentException("A store with the name: " + storeName + " does not exists in the system");
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
		{
			throw new ArgumentException("The member id: " + memberId + " does not exists in the system");
		}

		openStoresMutex.WaitOne();

		string errorDescription = CanAddOpenStore(storeName);
		if (errorDescription != null){
			openStoresMutex.ReleaseMutex();
			throw new ArgumentException(errorDescription); 
        }

		int newStoreId = GenerateStoreId(); 
		openStores.Add(newStoreId, new Store(storeName, storeFounder, (memberId) => membersController.GetMember(memberId)));

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
			throw new ArgumentException("An open store with an id: " + storeId + " does not exist in the system");
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

  //  // r 2.2, r 4.9
  //  // returns the search results from open stores by their ids
  //  public IDictionary<int, IList<Product>> SearchInOpenStores(ProductsSearchFilter filter)
  //  {
		//IEnumerable<KeyValuePair<int, Store>> passedStoresFilter = openStores.Where(pair => filter.FilterStore(pair.Value));

		//IDictionary<int, IList<Product>> result = new Dictionary<int, IList<Product>>();
		
		//IList<Product> passedProductsFilter; 
		//foreach (KeyValuePair<int, Store> idStorepair in passedStoresFilter)
  //      {
		//	passedProductsFilter = idStorepair.Value.Filter...();
		//	if (passedProductsFilter.Count > 0)
  //          {
		//		result[idStorepair.Key] = passedProductsFilter;
		//	}
  //      }
		//return result;
  //  }

    //// r 2.2, r 4.9
    //// returns the search results in every open store that satosfieds the predicat by its id
    //private IDictionary<int, IList<T>> SearchInOpenStores<T>(Func<Store, IList<T>> storeSerach, Predicate<Store> pred)
    //   {
    //	throw new Exception();
    //}
}
