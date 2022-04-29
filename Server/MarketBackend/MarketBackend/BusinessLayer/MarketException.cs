using System;
namespace MarketBackend.BusinessLayer.Market.StoreManagment
{
    public class MarketException : Exception
    {
        public MarketException():base() { }
        public MarketException(string message) : base(message) { }


    }
}