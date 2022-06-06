using MarketBackend.BusinessLayer.Market.StoreManagment;
using System;

public interface IExternalPaymentSystem
{
    public Task<int> Pay(PaymentDetails paymentDetails);
    public Task<int> CancelPay(int transactionId);
}
