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

	public bool makePayment()
    {
		return paymentsSystem.makePayment();
    }

	public bool makeDelivery()
    {
		return supplySystem.supplyDelivery();
	}

}
