using System;
using System.Collections.Generic;
namespace MarketBackend.BusinessLayer.Market.StoreManagment
{
	public class Hierarchy<T>
	{
		public T value { get; set; }
		public IList<Hierarchy<T>> children { get; }
		public Hierarchy<T> parent { get; }

		private Mutex hierarchyMutex; 

		public Hierarchy(T value)
		{
			this.value = value;
			this.children = new SynchronizedCollection<Hierarchy<T>>();
			this.parent = null;
			hierarchyMutex = new Mutex();
		}
		public Hierarchy(T value, Hierarchy<T> parent)
		{
			this.value = value;
			this.children = new SynchronizedCollection<Hierarchy<T>>();
			this.parent = parent;
			hierarchyMutex = new Mutex();
		}

		// r.4.4
		// r.4.6
		public void AddToHierarchy(T adder, T valueToAdd) 
		{
			// adder is a node somewhere in the current Hierarchy
			// which attemps to add a *new* child (valueToAdd) to it's own Hierarchy

			hierarchyMutex.WaitOne();
			Hierarchy<T> adderHierarchy = FindHierarchy(adder);
			if (adderHierarchy == null) { 
				hierarchyMutex.ReleaseMutex();
				throw new MarketException("the adder isn't in the hierarchy");
			}
			
			Hierarchy<T> valueHierarchy = FindHierarchy(valueToAdd);
			if (valueHierarchy != null) {  // will prevent cyclic assignments
				hierarchyMutex.ReleaseMutex();
				throw new MarketException("allready in the hirearchy");
			}

			adderHierarchy.children.Add(new Hierarchy<T>(valueToAdd, adderHierarchy));
			hierarchyMutex.ReleaseMutex();
		}

		// r.4.5
		// r.4.8
		public Hierarchy<T> RemoveFromHierarchy(T remover, T valueToRemove)
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
				throw new MarketException("the remover doesn't have the appropriate hierarchy classification");
			}
			Hierarchy<T> valueHierarchyParent = valueHierarchy.parent;
			if (valueHierarchyParent != null) {
				valueHierarchyParent.children.Remove(valueHierarchy);
			}

			hierarchyMutex.ReleaseMutex();
			return valueHierarchy;
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
