using MarketBackend.BusinessLayer.Market.StoreManagment;
using MarketBackend.BusinessLayer.System.ExternalServices;


// this class right now has a deault implementation because of the fact we dont have an actual supply/payment
// systems we can use in our market.
public class ExternalServicesController
{
	private IExternalPaymentSystem paymentsSystem;
	private IExternalSupplySystem supplySystem;
	public ExternalServicesController(IExternalPaymentSystem ep, IExternalSupplySystem es)
	{
		paymentsSystem = ep;
		supplySystem = es;
	}

	public virtual int makePayment(PaymentDetails paymentDetails)
    {
		return paymentsSystem.Pay(paymentDetails).Result;
    }
	
	public virtual int CancelPayment(int transactionId)
    {
		return paymentsSystem.CancelPay(transactionId).Result;
    }

	public virtual int makeDelivery(SupplyDetails supplyDetails)
    {
		return supplySystem.Supply(supplyDetails).Result;
	}

	public virtual int CancelDelivery(int transactionId)
    {
		return supplySystem.CancelSupply(transactionId).Result;
    }
	

}
