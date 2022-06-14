using MarketBackend.DataLayer.DataDTOs.Market.StoreManagement;
using MarketBackend.DataLayer.DataManagers;
using System;
using System.Collections.Generic;
namespace MarketBackend.BusinessLayer.Market.StoreManagment
{
	public class Hierarchy<T>
	{
		private int id; 
		public T value { get; set; }
		public IList<Hierarchy<T>> children { get; }
		public Hierarchy<T> parent { get; }

		private Mutex hierarchyMutex;

		private Action<DataAppointmentsNode, T> initializeValue; 
		private HierarchyDataManager hierarchyDataManager;

		private const int ID_COUNTER_NOT_INITIALIZED = -1;
		private static int idCounter = ID_COUNTER_NOT_INITIALIZED;
		private static Mutex counterLock = new Mutex();

		private static int GetNextId()
		{
			int temp; 
			counterLock.WaitOne();

			if (idCounter == ID_COUNTER_NOT_INITIALIZED)
				InitializeIdCounter();

			temp = idCounter;
			idCounter++;

			counterLock.ReleaseMutex();

			return temp;
		}

		private static void InitializeIdCounter()
		{
			idCounter = HierarchyDataManager.GetInstance().GetNextId();
		}

		private Hierarchy(int id, T value, Action<DataAppointmentsNode, T> initializeValue, Hierarchy<T> parent = null)
		{
			this.id = id; 
			this.value = value;
			this.children = new SynchronizedCollection<Hierarchy<T>>();
			this.parent = parent;
			hierarchyMutex = new Mutex();

			this.initializeValue = initializeValue;
			this.hierarchyDataManager = HierarchyDataManager.GetInstance();
		}

		public Hierarchy(T value, Action<DataAppointmentsNode, T> initializeValue, Hierarchy<T> parent = null) 
			: this(GetNextId(), value, initializeValue, parent)
		{
			
		}

		// r S 8
		public static Hierarchy<int> DataHierarchyToHierarchy(DataAppointmentsNode dataHierarchy, Hierarchy<int> parent = null)
		{
			Hierarchy<int> result = new Hierarchy<int>(dataHierarchy.Id, dataHierarchy.MemberId, (dataHierarchy, value) => dataHierarchy.MemberId = value, parent);
			result.AddChildrenNoSave(dataHierarchy.Children.
				Select(childDataHierarchy =>
					DataHierarchyToHierarchy(childDataHierarchy, result))
				.ToList());
			return result;
		}

		public DataAppointmentsNode ToNewDataAppointmentsNode()
		{
			DataAppointmentsNode result = new DataAppointmentsNode()
			{
				Id = id, 
				Children = children.Select(child => child.ToNewDataAppointmentsNode()).ToList()
			};
			initializeValue(result, value);
			return result; 
        }

		private void AddChildrenNoSave(IList<Hierarchy<T>> childrenToAdd)
        {
			foreach(Hierarchy<T> child in childrenToAdd)
            {
				this.children.Add(child);
			}
        }

		// r.4.4
		// r.4.6
		public void AddToHierarchy(T adder, T valueToAdd, Action saveChanges)
		{
			// adder is a node somewhere in the current Hierarchy
			// which attemps to add a *new* child (valueToAdd) to it's own Hierarchy

			hierarchyMutex.WaitOne();
			Hierarchy<T> adderHierarchy = FindHierarchy(adder);
			if (adderHierarchy == null)
			{
				hierarchyMutex.ReleaseMutex();
				throw new MarketException("the adder isn't in the hierarchy");
			}

			Hierarchy<T> valueHierarchy = FindHierarchy(valueToAdd);
			if (valueHierarchy != null)
			{  // will prevent cyclic assignments
				hierarchyMutex.ReleaseMutex();
				throw new MarketException("allready has a role");
			}

			Hierarchy<T> newHierarchy = new Hierarchy<T>(valueToAdd, initializeValue, adderHierarchy);
			DataAppointmentsNode dataHierearchy = newHierarchy.ToNewDataAppointmentsNode();
			hierarchyDataManager.Add(dataHierearchy);

			saveChanges(); 

			adderHierarchy.children.Add(newHierarchy);
			hierarchyMutex.ReleaseMutex();
		}

		// r.4.5
		// r.4.8
		public Hierarchy<T> RemoveFromHierarchy(T remover, T valueToRemove, Action saveChanges)
		{
			// remover is a node somewhere in the current Hierarchy
			// which attemps to remove existing child (valueToRemove) from it's own Hierarchy

			hierarchyMutex.WaitOne();
			
			Hierarchy<T> removerHierarchy = FindHierarchy(remover);
			if (removerHierarchy == null) { 
				hierarchyMutex.ReleaseMutex();
				throw new MarketException("the remover isn't in the hierarchy");
			}
			Hierarchy<T> valueHierarchy = removerHierarchy.FindHierarchy(valueToRemove);
			if (valueHierarchy == null) { 
				hierarchyMutex.ReleaseMutex();
				throw new MarketException("Sorry, but this is not a valid remove request");
			}
			Hierarchy<T> valueHierarchyParent = valueHierarchy.parent;
			if (valueHierarchyParent != null) {

				DataRemove(); 
				saveChanges();

				valueHierarchyParent.children.Remove(valueHierarchy);
			}

			hierarchyMutex.ReleaseMutex();
			return valueHierarchy;
		}

		public IList<T> GetHierarchyValueList()
        {
			IList<T> hierarchyIdList = children.SelectMany(child => child.GetHierarchyValueList()).ToList();
			hierarchyIdList.Insert(0, value);
			return hierarchyIdList; 
		}

		private void DataRemove()
        {
			hierarchyDataManager.Remove(id);
			foreach (Hierarchy<T> child in children)
            {
				child.DataRemove(); 
            }
		}

		public Hierarchy<T> FindHierarchy(T node)
		{
			// base case
			if (value.Equals(node))
			{
				return this;
			}
			if (children.Count == 0)
			{
				return null;
			}

			// recursion step
			foreach (Hierarchy<T> child in children)
			{
				Hierarchy<T> findFromChild = child.FindHierarchy(node);
				if (findFromChild != null)
					return findFromChild;
			}

			// last case, means that value isn't in this Hierarchy
			return null;
		}
	}
}
