using System;

public class Hierarchy<T>
{
	private List<Hierarchy<T>> children;
	public Hierarchy()
	{
		children = new List<Hierarchy<T>>();
	}
}
