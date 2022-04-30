using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.BusinessLayer.System.ExternalServices
{
    // this class will hold the outside payment system when we will have it
    // for now its default to returning true to allow the system to operate normally
    internal class ExternalPaymentSystem : IExternalPaymentSystem
    {
        public ExternalPaymentSystem()
        {

        }

        // will contact the external service to make the payment, for now deafult
        public bool makePayment()
        {
            return true;
        }
    }
}
