using MarketBackend.BusinessLayer.Buyers.Members;
using System;

public class Store
{
    private string name; 

	public Store(string storeName, Member founder)
	{
        this.name = storeName;
	}

    public virtual string GetName()
    {
        return name; 
    }

    public void CloseStore(int memberId)
    {
        // todo: implement
    }
}
