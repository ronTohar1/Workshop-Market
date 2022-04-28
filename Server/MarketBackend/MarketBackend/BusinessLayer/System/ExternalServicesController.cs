using MarketBackend.BusinessLayer.Market.StoreManagment;
using System;
using System.Collections.Concurrent;

public class ExternalServicesController
{
	private IDictionary<int, IExternalPaymentSystem> paymentsSystems;
	private IDictionary<int, IExternalSupplySystem> supplySystems;
	public ExternalServicesController()
	{
		paymentsSystems = new ConcurrentDictionary<int, IExternalPaymentSystem>();
		supplySystems = new ConcurrentDictionary<int, IExternalSupplySystem>();
	}

	public bool makePayment(int id)
    {
		if (!paymentsSystems.ContainsKey(id))
        {
			throw new MarketException($"No payment system with id {id} in the external payment systems");
		}
		return paymentsSystems[id].makePayment();
    }

	public bool makeDelivery(int id)
    {
		if (!supplySystems.ContainsKey(id))
        {
			throw new MarketException($"No supply system with id {id} in the external supply systems");
		}
		return supplySystems[id].supplyDelivery();
	}

}
