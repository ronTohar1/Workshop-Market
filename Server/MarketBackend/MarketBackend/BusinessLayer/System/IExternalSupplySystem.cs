using MarketBackend.BusinessLayer.Market.StoreManagment;
using System;

public interface IExternalSupplySystem
{
    public Task<int> Supply(SupplyDetails supplyDetails);
    public Task<int> CancelSupply(int transactionId);
}
