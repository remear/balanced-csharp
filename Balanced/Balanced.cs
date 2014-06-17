using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace Balanced
{
    public static class Balanced
    {
        private static string VERSION = "1.0";
        private static string API_REVISION = "1.1";
        private static string API_URL = "https://api.balancedpayments.com";
        private static string AGENT = "balanced-csharp";
        internal static string API_KEY { get; set; }

        public static void configure(string apiKey)
        {
            API_KEY = apiKey;
        }

        public static string getAPIURL()
        {
            return API_URL;
        }

        public static string getVersion()
        {
            return VERSION;
        }

        public static void crap()
        {
            var customer = new Customer();
            Console.Write(customer.email);
        }

    }
}
