using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using NLog.Config;
namespace SystemLog
{
    class SystemLogger
    {
        private static Logger? _logger=null;
        public static Logger getLogger()
        {
            if (_logger == null)
            {
                //---------------- log setup: done only once when the system comes up --------------
                string currentPath = Directory.GetCurrentDirectory();
                string new_path = Path.Combine(currentPath, @"..\MarketBackend\", @"SystemLog");


                var config = new NLog.Config.LoggingConfiguration();

                // Targets where to log to: File and Console
                var logfile1 = new NLog.Targets.FileTarget("logfile1") { FileName = Path.Combine(new_path, "event_logs.txt") };
                var logfile2 = new NLog.Targets.FileTarget("logfile2") { FileName = Path.Combine(new_path, "error_logs.txt") };

                // Rules for mapping loggers to targets            
                config.AddRule(LogLevel.Info, LogLevel.Info, logfile1);
                config.AddRule(LogLevel.Error, LogLevel.Error, logfile2);


                // Apply config           
                NLog.LogManager.Configuration = config;
                //---------------------------------end of log setup --------------------------------
                _logger = NLog.LogManager.GetCurrentClassLogger();
            }
            return _logger;
        }
    }
}
