using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Balanced
{
    public class Refund : Resource
    {
        [JsonIgnore]
        public static string resource_href
        {
            get { return "/refunds"; }
        }

        // fields
        public int amount { get; set; }
        public string description { get; set; }
        
        // attributes
        [JsonIgnore]
        public string currency { get; set; }
        [JsonIgnore]
        public string status { get; set; }
        [JsonIgnore]
        public string transaction_number { get; set; }

        [JsonIgnore]
        [ResourceField(field = "refunds.debit")]
        public Debit debit { get; set; }

        [JsonIgnore]
        [ResourceField(field = "refunds.dispute")]
        public Dispute dispute { get; set; }

        [JsonIgnore]
        [ResourceField(field = "refunds.events")]
        public Event.Collection events { get; set; }

        //[JsonIgnore]
        //[ResourceField(field = "refunds.order")]
        //public Order order { get; set; }

        public Refund() { }

        public Refund(Dictionary<string, object> payload) { }

        public static Refund Fetch(string href)
        {
            return Resource.Fetch<Refund>(href, null);
        }

        public static Refund Fetch(string href, Dictionary<string, string> queryParams)
        {
            return Resource.Fetch<Refund>(href, queryParams);
        }

        public Refund save()
        {
            return this.save<Refund>();
        }

        public class Collection : ResourceCollection<Refund>
        {
            public Collection() : base(resource_href) { }
            public Collection(string href) : base(href) { }
        }
    }
}
