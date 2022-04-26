using System;
namespace MarketBackend.BusinessLayer.Market.StoreManagment
{ 
public class Product
{
	private string name;
	private Dictionary<string, double> discounts; //mapping between discount code and discount percentage 
	private int amountInInventory;
	public Product(string product_name)
	{
		name = product_name;
		discounts = new Dictionary<string, double>();
		amountInInventory = 0;
	}
	public void addNewDiscount(string discount_code, double discount_percentage) { 
		discounts[discount_code] = discount_percentage;
	}
	public void addToInventory(int amountToAdd) { 
		amountInInventory = amountInInventory + amountToAdd;
	} 
	public void removeFromInventory(int amountToRemove) { 
		if (amountInInventory<amountToRemove)
			throw new StoreManagmentException($"Not enough products of {name} in storage");
		else
			amountInInventory = amountInInventory - amountToRemove;
	} 
}
}