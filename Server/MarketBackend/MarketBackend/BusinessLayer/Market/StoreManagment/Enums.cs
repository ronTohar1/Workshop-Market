using System;
namespace MarketBackend.BusinessLayer.Market.StoreManagment
{
	public enum Role
	{
		Manager,
		Owner,
		Founder
	};

	public enum Permission
	{
		RecieveInfo,
		MakeCoManager,
		RemoveCoManager,
		MakeCoOwner,
		RemoveCoOwner
	};
	public enum PurchaseOption
	{
		Public,
		Bid,
		Immediate,
		Raffle
	};
}