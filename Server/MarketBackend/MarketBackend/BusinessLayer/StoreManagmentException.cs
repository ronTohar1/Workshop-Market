using System;
namespace MarketBackend.BusinessLayer.Market.StoreManagment
{
    public class StoreManagmentException : Exception
    {
        public StoreManagmentException():base() { }
        public StoreManagmentException(string message) : base(message) { }


    }
}