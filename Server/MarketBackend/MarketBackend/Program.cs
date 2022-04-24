using MarketBackend.BusinessLayer.Buyers.Guests;
using System;
using NLog;
using NLog.Config;
namespace MyApp // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Example example = new Example();
            Console.WriteLine("Hello World!");
            if (example.IsEven(2))
                Console.WriteLine($"the number 2 is even");
            else
                Console.WriteLine($"the number 2 is not even");
            
            log_example();
        }
        static void log_example()
        {
            //---------------- log setup: done only once when the system comes up --------------
            var path = Directory.GetCurrentDirectory();

            var config = new NLog.Config.LoggingConfiguration();

            // Targets where to log to: File and Console
            var logfile1 = new NLog.Targets.FileTarget("logfile1") { FileName = Path.Combine(path, "logs", "event_logs.txt") };
            var logfile2 = new NLog.Targets.FileTarget("logfile2") { FileName = Path.Combine(path, "logs", "error_logs.txt") };

            // Rules for mapping loggers to targets            
            config.AddRule(LogLevel.Info, LogLevel.Info, logfile1);
            config.AddRule(LogLevel.Error, LogLevel.Error, logfile2);

            // Apply config           
            NLog.LogManager.Configuration = config;
            //---------------------------------end of log setup --------------------------------

            var logger = NLog.LogManager.GetCurrentClassLogger();//thread safe
            try
            {
                logger.Info($"Guest with id: {123} purchased a {"banana"} !");
                throw new Exception("BOOM!!!");
            }
            catch (Exception ex)
            {
                logger.Error($"Guest with id: {123} doesn't have the right permission");
            }

        }
    }
}