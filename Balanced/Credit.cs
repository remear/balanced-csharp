using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Balanced
{
    public class Credit : Resource
    {
        [JsonIgnore]
        public static string resource_href
        {
            get { return "/credits"; }
        }

        public int amount { get; set; }
        public string appears_on_statement_as { get; set; }

        [JsonIgnore]
        public string currency { get; set; }
        public string description { get; set; }
        public string failure_reason { get; set; }
        public string failure_reason_code { get; set; }
        public string status { get; set; }
        public string transaction_number { get; set; }

        //[ResourceField(field="credits.customer")]
        //public Customer customer { get; set; }

        public Credit() { }

        public Credit(Dictionary<string, object> payload) { }

        public static Credit Fetch(string href)
        {
            return Resource.Fetch<Credit>(href, null);
        }

        public static Credit Fetch(string href, Dictionary<string, string> queryParams)
        {
            return Resource.Fetch<Credit>(href, queryParams);
        }

        public Credit save()
        {
            return this.save<Credit>();
        }

        public class Collection : ResourceCollection<Credit>
        {
            public Collection() : base(resource_href) { }
            public Collection(string href) : base(href) { }
        }
    }
}
