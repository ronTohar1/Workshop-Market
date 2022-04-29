using System;
namespace MarketBackend.BusinessLayer.Market.StoreManagment
{
	public enum Role
	{
		Manager,
		Owner
	};

	public enum Permission
	{
		RecieveInfo,
		MakeCoManager,
		RemoveCoManager,
		MakeCoOwner,
		RemoveCoOwner, 
		RecieiveRolesInfo
	};
	public enum PurchaseOption
	{
		Public,
		Bid,
		Immediate,
		Raffle
	};
}