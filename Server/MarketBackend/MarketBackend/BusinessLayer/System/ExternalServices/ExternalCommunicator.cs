using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.BusinessLayer.System.ExternalServices
{
    public class ExternalCommunicator
    {

        private const string URL = "https://cs-bgu-wsep.herokuapp.com/";

        private static IDictionary<string, string> handshakeContent = new Dictionary<string, string>()
        {
            {"action_type", "handshake" }
        };

        private HttpClient httpClient;

        public ExternalCommunicator(HttpClient httpClient) => this.httpClient = httpClient;

        protected bool handshake()
        {
            return post(handshakeContent).Result.IsSuccessStatusCode;
        }

        protected async Task<HttpResponseMessage> post(IDictionary<string, string> postParameters)
        {
            var content = new FormUrlEncodedContent(postParameters);

            var response = await httpClient.PostAsync(URL, content);

            return response;
        }
    }
}
