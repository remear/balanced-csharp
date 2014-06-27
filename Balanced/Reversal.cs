using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Balanced
{
    public class Reversal : Resource
    {
        [JsonIgnore]
        public static string resource_href
        {
            get { return "/reversals"; }
        }

        // fields
        public int amount { get; set; }
        public string description { get; set; }

        // attributes
        [JsonIgnore]
        public string currency { get; set; }
        [JsonIgnore]
        public string failure_reason { get; set; }
        [JsonIgnore]
        public string failure_reason_code { get; set; }
        [JsonIgnore]
        public string status { get; set; }
        [JsonIgnore]
        public string transaction_number { get; set; }

        [JsonIgnore]
        [ResourceField(field = "reversals.credit")]
        public Credit credit { get; set; }

        [JsonIgnore]
        [ResourceField(field = "reversals.events")]
        public Event.Collection events { get; set; }

        //[JsonIgnore]
        //[ResourceField(field = "reversals.order")]
        //public Order order { get; set; }

        public Reversal() { }

        public Reversal(Dictionary<string, object> payload) { }

        public static Reversal Fetch(string href)
        {
            return Resource.Fetch<Reversal>(href, null);
        }

        public static Reversal Fetch(string href, Dictionary<string, string> queryParams)
        {
            return Resource.Fetch<Reversal>(href, queryParams);
        }

        public Reversal save()
        {
            return this.save<Reversal>();
        }

        public class Collection : ResourceCollection<Reversal>
        {
            public Collection() : base(resource_href) { }
            public Collection(string href) : base(href) { }
        }
    }
}
