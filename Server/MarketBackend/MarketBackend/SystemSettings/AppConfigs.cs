using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.SystemSettings
{
    public class AppConfigs
    {
        #region Singleton
        private static AppConfigs? instance = null;
        public static AppConfigs GetInstance()
        {
            if (instance == null)
                instance = new AppConfigs();
            return instance;
        }
        private AppConfigs() 
        {
            ParseConfigs();
        }
        #endregion

        #region Parsing
        private static string ParseString(string name)
        {
            var val = ConfigurationManager.AppSettings[name];
            if (val == null)
                throw new ConfigurationErrorsException($"Unable to parse user configs - field [{name}]");
            return val;
        }
        private static bool ParseBool(string name)
        {
            var val = ConfigurationManager.AppSettings[name];
            if (val == null)
                throw new ConfigurationErrorsException($"Unable to parse user configs - field [{name}]");
        
            if (val.ToLower() == "true")
                return true;
            if (val.ToLower() == "false")  
                return false;

            throw new ConfigurationErrorsException($"Unable to parse user configs - boolean field [{name}] should be 'true' or 'false'");
        }
        private static int ParseInt(string name)
        {
            var val = ConfigurationManager.AppSettings[name];
            if (val == null)
                throw new ConfigurationErrorsException($"Unable to parse user configs - field [{name}]");
            return int.Parse(val);
        }

        private void ParseConfigs()
        {
            DatabaseName         = ParseString("DatabaseName");
            DatabaseInstanceName = ParseString("DatabaseInstanceName");
            DatabaseIp           = ParseString("DatabaseIp");
            DatabasePort         = ParseString("DatabasePort");
            DatabaseUsername     = ParseString("DatabaseUsername");
            DatabasePassword     = ParseString("DatabasePassword");
            ShouldUpdateDatabase = ParseBool("ShouldUpdateDatabase");
            ShouldRunInitFile    = ParseBool("ShouldRunInitFile");
            InitFilePath         = ParseString("InitFilePath");
            WebsocketServerPort  = ParseInt("WebsocketServerPort");
        }
        #endregion

        #region Configs
        public string DatabaseName { get; private set; }
        public string DatabaseInstanceName { get; private set; }
        public string DatabaseIp { get; private set; }
        public string DatabasePort { get; private set; }
        public string DatabaseUsername { get; private set; }
        public string DatabasePassword { get; private set; }
        public bool ShouldUpdateDatabase { get; private set; }
        public bool ShouldRunInitFile { get; private set; }
        public string InitFilePath { get; private set; }
        public int WebsocketServerPort { get; private set; }
        #endregion
    }
}
