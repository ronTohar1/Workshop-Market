using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Reflection;

namespace MarketBackend.SystemSettings
{
    public class AppConfigs
    {
        private static readonly string PATH = Path.Combine(Environment.CurrentDirectory.Substring(0, Environment.CurrentDirectory.IndexOf("MarketBackend") + "MarketBackend".Length), "appconfig.json");

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
            Console.WriteLine("path = " + PATH);
            string jsonText = File.ReadAllText(PATH);
            JsonConfigObject = JsonConvert.DeserializeObject<dynamic>(jsonText)!;
            ParseConfigs();
        }
        #endregion

        #region Parsing
        private static dynamic JsonConfigObject { get; set; }

        private static T Parse<T>(string name)
        {
            var val = (T)JsonConfigObject[name];
            if (val == null)
                throw new ConfigurationErrorsException($"Unable to parse user configs - field [{name}]");
            return val;
        }

        private void ParseConfigs()
        {
            DatabaseName = Parse<string>("DatabaseName");
            DatabaseInstanceName = Parse<string>("DatabaseInstanceName");
            DatabaseIp = Parse<string>("DatabaseIp");
            DatabasePort = Parse<string>("DatabasePort");
            DatabaseUsername = Parse<string>("DatabaseUsername");
            DatabasePassword = Parse<string>("DatabasePassword");
            ShouldRunInitFile = Parse<bool>("ShouldRunInitFile");
            ShouldUpdateDatabase = Parse<bool>("ShouldUpdateDatabase");
            InitFilePath = Parse<string>("InitFilePath");
            WebsocketServerPort = Parse<int>("WebsocketServerPort");
            ExternalServicesActive = Parse<bool>("ExternalServicesActive");
            ExternalServicesFailWhenNotActive = Parse<bool>("ExternalServicesFailWhenNotActive");

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
        public bool ExternalServicesActive { get; private set; }
        public bool ExternalServicesFailWhenNotActive { get; private set; }
        #endregion
    }
}
