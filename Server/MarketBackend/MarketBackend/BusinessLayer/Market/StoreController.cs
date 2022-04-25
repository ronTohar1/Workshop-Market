﻿using System;

internal class StoreController
{
	private Dictionary<int, Store> openStores;
	private Dictionary<int, Store> closedStores; 

	private MembersController membersController;

	private static int storeIdCounter = 0; // the next store id

	// creates a new StoreController without stores yet
	internal StoreController(MembersController membersController)
	{
		this.membersController = membersController;

		this.openStores = new Dictionary<int, Store>(); 
		this.closedStores = new Dictionary<int, Store>();
	}

	internal Store GetStore(int storeId)
    {
		return null;
    }

	internal int GetStoreIdByName(string storeName)
    {
		// The stores in the system have a unique name for simplicity todo: check when adding a store
		return -1; 
	}

	// opens a new store and returns its id
	internal int OpenNewStore(int memberId, string storeName)
    {
		return -1; 
    }


}