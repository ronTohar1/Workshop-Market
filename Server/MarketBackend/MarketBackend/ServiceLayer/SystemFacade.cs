using MarketBackend.ServiceLayer.ServiceDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.ServiceLayer
{
    internal class SystemFacade
    {
        public SystemFacade()
        {

        }

        public Response<bool> OpenMarket()
        {
            return new Response<bool>();
        }

        public Response<int> AddNewSupplier(string supplierName)
        {
            return new Response<int>();
        }

        public Response<bool> RemoveSupplier(string supplierName)
        {
            return new Response<bool>();
        }

        public Response<bool> SerSupplierInfo(string supplierName)
        {
            return new Response<bool>();
        }

        public Response<int> AddNewPaymentService(string paymentServiceName)
        {
            return new Response<int>();
        }

        public Response<bool> RemovePaymentService(string paymentServiceName)
        {
            return new Response<bool>();
        }

        public Response<bool> SetPaymentServiceInfo(string paymentServiceName)
        {
            return new Response<bool>();
        }


    }
}
