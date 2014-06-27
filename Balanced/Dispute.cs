using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Balanced
{
    public class Dispute : Resource
    {
        [JsonIgnore]
        public static string resource_href
        {
            get { return "/disputes"; }
        }

        // attributes
        [JsonIgnore]
        public int amount { get; set; }
        [JsonIgnore]
        public string currency { get; set; }
        [JsonIgnore]
        public DateTime initiated_at { get; set; }
        [JsonIgnore]
        public string reason { get; set; }
        [JsonIgnore]
        public DateTime respond_by { get; set; }
        [JsonIgnore]
        public string status { get; set; }

        [JsonIgnore]
        [ResourceField(field="disputes.events")]
        public Event.Collection events { get; set; }

        [JsonIgnore]
        [ResourceField(field = "disputes.transaction")]
        public Debit transaction { get; set; }


        public static Dispute Fetch(string href)
        {
            return Resource.Fetch<Dispute>(href, null);
        }

        public static Dispute Fetch(string href, Dictionary<string, string> queryParams)
        {
            return Resource.Fetch<Dispute>(href, queryParams);
        }

        public class Collection : ResourceCollection<Dispute>
        {
            public Collection() : base(resource_href) { }
            public Collection(string href) : base(href) { }
        }
    }
}
