using System;
namespace MarketBackend.BusinessLayer
{
    public class MarketException : Exception
    {
        public MarketException():base() { }
        public MarketException(string message) : base(message) { }


    }
}