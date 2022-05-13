using MarketBackend.BusinessLayer.Buyers.Guests;
using System;
using NLog;
using SystemLog;
namespace MyApp // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
            log_example();
        }

        static void log_example()
        {
            Logger logger = SystemLogger.getLogger();
            try
            {
                logger.Info($"Guest with id: {123} purchased a {"banana"} !");
                throw new Exception("BOOM!!!");
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"Guest with id: {123} doesn't have the right permission");
            }

        }
    }
}