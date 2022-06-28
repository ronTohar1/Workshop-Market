using MarketBackend.BusinessLayer.Market.StoreManagment;
using MarketBackend.SystemSettings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.BusinessLayer.System.ExternalServices
{
    // this class will hold the outside payment system when we will have it
    // for now its default to returning true to allow the system to operate normally
    public class ExternalSupplySystem : ExternalCommunicator, IExternalSupplySystem
    {

        private AppConfigs appConfig = AppConfigs.GetInstance();

        public ExternalSupplySystem(HttpClient httpClient) : base(httpClient) { }

        // will contact the external service for the delivery, for now default
        public async virtual Task<int> Supply(SupplyDetails supplyDetails)
        {
            if (!appConfig.ExternalSystemsActive)
                return -1;

            if (!handshake())
                return -1;

            IDictionary<string, string> supplyContent = new Dictionary<string, string>()
            {
                {"action_type", "supply" },
                {"name", supplyDetails.Name },
                {"address", supplyDetails.Address },
                {"city", supplyDetails.City },
                {"country", supplyDetails.Country },
                {"zip", supplyDetails.Zip },
            };

            string response = await post(supplyContent).Result.Content.ReadAsStringAsync();

            return int.Parse(response);
        }

        public async Task<int> CancelSupply(int transactionId)
        {
            if (!appConfig.ExternalSystemsActive)
                return -1;

            if (!handshake())
                return -1;

            IDictionary<string, string> cancelSupplyContent = new Dictionary<string, string>()
            {
                {"action_type", "cancel_supply" },
                {"transaction_id", transactionId.ToString() },
            };

            string response = await post(cancelSupplyContent).Result.Content.ReadAsStringAsync();

            return int.Parse(response);
        }

    }
}
