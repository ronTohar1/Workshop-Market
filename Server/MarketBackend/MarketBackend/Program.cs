using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using MarketBackend.SystemSettings;
using Newtonsoft.Json;

namespace MarketBackend
{
    public class Program
    {

        public static void Main(String[] args)
        {
            Console.WriteLine("Hello, Market!");
            AppConfigs appConfigs = AppConfigs.GetInstance();
            Console.WriteLine(appConfigs.DatabaseName);
        }
    }
}
