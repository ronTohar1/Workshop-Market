using System;
namespace MarketBackend.BusinessLayer.Market.StoreManagment
{
	public class Hierarchy<T>
	{
		public T value { get; set; }
		public List<Hierarchy<T>> children { get; }
		public Hierarchy<T> parent { get; }

		private Mutex childrenMutex; //we will use this Mutex every time we would like to change the children List

		public Hierarchy(T value)
		{
			this.value = value;
			this.children = new List<Hierarchy<T>>();
			this.parent = null;
			childrenMutex = new Mutex();
		}
		public Hierarchy(T value, Hierarchy<T> parent)
		{
			this.value = value;
			this.children = new List<Hierarchy<T>>();
			this.parent = parent;
			childrenMutex = new Mutex();
		}

		// r.4.4
		// r.4.6
		public void AddToHierarchy(T adder, T valueToAdd) 
		{
			// adder is a node somewhere in the current Hierarchy
			// which attemps to add a *new* child (valueToAdd) to it's own Hierarchy

			childrenMutex.WaitOne();
			Hierarchy<T> adderHierarchy = FindHierarchy(adder);
			if (adderHierarchy == null) { 
				childrenMutex.ReleaseMutex();
				throw new StoreManagmentException("the adder isn't in the hierarchy");
			}
			
			Hierarchy<T> valueHierarchy = FindHierarchy(valueToAdd);
			if (valueHierarchy != null) {  // will prevent cyclic assignments
				childrenMutex.ReleaseMutex();
				throw new StoreManagmentException("allready in the hirearchy");
			}

			adderHierarchy.children.Add(new Hierarchy<T>(valueToAdd, adderHierarchy));
			childrenMutex.ReleaseMutex();
		}

		// r.4.5
		// r.4.8
		public void RemoveFromHierarchy(T remover, T valueToRemove)
		{
			// remover is a node somewhere in the current Hierarchy
			// which attemps to remove existing child (valueToRemove) from it's own Hierarchy

			childrenMutex.WaitOne();
			
			Hierarchy<T> removerHierarchy = FindHierarchy(remover);
			if (removerHierarchy == null) { 
				childrenMutex.ReleaseMutex();
				throw new StoreManagmentException("the remover isn't in the hierarchy");
			}
			Hierarchy<T> valueHierarchy = removerHierarchy.FindHierarchy(valueToRemove);
			if (valueHierarchy == null) { 
				childrenMutex.ReleaseMutex();
				throw new StoreManagmentException("the remover doesn't have the appropriate hierarchy classification");
			}
			Hierarchy<T> valueHierarchyParent = valueHierarchy.parent;
			if (valueHierarchyParent != null) {
				valueHierarchyParent.children.Remove(valueHierarchy);
			}

			childrenMutex.ReleaseMutex();

		}

		public Hierarchy<T> FindHierarchy(T node)
		{
			// base case
			if (this.value.Equals(node))
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
