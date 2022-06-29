using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketBackend.BusinessLayer.Market.StoreManagment;
using MarketBackend.SystemSettings;

namespace MarketBackend.BusinessLayer.System.ExternalServices
{
    // this class will hold the outside payment system when we will have it
    // for now its default to returning true to allow the system to operate normally
    public class ExternalPaymentSystem : ExternalCommunicator, IExternalPaymentSystem
    {

        private AppConfigs appConfig = AppConfigs.GetInstance();

        public ExternalPaymentSystem(HttpClient httpClient) : base(httpClient) { }

        // will contact the external service to make the payment, for now deafult
        public async virtual Task<int> Pay(PaymentDetails paymentDetails)
        {
            if (!appConfig.ExternalServicesActive) { 
                if (appConfig.ExternalServicesFailWhenNotActive)
                    return -1;
                return new Random().Next();
            }

            if (!handshake())
                return -1;

            IDictionary<string, string> payContent = new Dictionary<string, string>()
            {
                {"action_type", "pay" },
                {"card_number", paymentDetails.CardNumber },
                {"month", paymentDetails.Month },
                {"year", paymentDetails.Year },
                {"holder", paymentDetails.Holder },
                {"ccv", paymentDetails.Ccv },
                {"id", paymentDetails.Id },
            };

            string response = await post(payContent).Result.Content.ReadAsStringAsync();

            return int.Parse(response);
        }

        public async virtual Task<int> CancelPay(int transactionId)
        {
            if (!appConfig.ExternalServicesActive)
            {
                if (appConfig.ExternalServicesFailWhenNotActive)
                    return -1;
                return 0;
            }

            if (!handshake())
                return -1;

            IDictionary<string, string> cancelPayContent = new Dictionary<string, string>()
            {
                {"action_type", "cancel_pay" },
                {"transaction_id", transactionId.ToString() },
            };

            string response = await post(cancelPayContent).Result.Content.ReadAsStringAsync();

            return int.Parse(response);
        }

    }
}
