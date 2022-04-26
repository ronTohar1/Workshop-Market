using MarketBackend.BusinessLayer.Buyers.Members;
using System;
using System.Collections.Concurrent;

public class StoreController
{
	private IDictionary<int, Store> openStores;
	private IDictionary<int, Store> closedStores; 

	private MembersController membersController;

	private static int storeIdCounter = 0; // the next store id
	private static Mutex storeIdCounterMutex = new Mutex();

	private Mutex openStoresMutex; 

	// creates a new StoreController without stores yet
	public StoreController(MembersController membersController)
	{
		this.membersController = membersController;

		this.openStores = new ConcurrentDictionary<int, Store>(); 
		this.closedStores = new ConcurrentDictionary<int, Store>();

		this.openStoresMutex = new Mutex(); 
	}

	public Store GetStore(int storeId)
    {
		return null;
    }

	// r 2.1
	public int GetStoreIdByName(string storeName)
    {
		// The stores in the system have a unique name for simplicity todo: check when adding a store
		return -1; 
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
		openStores.Add(newStoreId, new Store(storeName, storeFounder));

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
	public void CloseStore(int storeId)
    {
		
    }

	public virtual bool StoreExists(int storeId)
    {
		return false; 
    }

	public virtual bool StoreExists(string storeName)
	{
		return false;
	}
}
