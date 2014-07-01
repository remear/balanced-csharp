using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Balanced
{
    public class Callback : Resource
    {
        [JsonIgnore]
        public static string resource_href
        {
            get { return "/callbacks"; }
        }

        // fields
        public string method { get; set; }
        public string url { get; set; }

        // attributes
        [JsonIgnore]
        public string revision { get; set; }


        public Callback() { }

        public Callback(Dictionary<string, object> payload) { }

        public static Callback Fetch(string href)
        {
            return Resource.Fetch<Callback>(href);
        }

        public class Collection : ResourceCollection<Callback>
        {
            public Collection() : base(resource_href) { }
            public Collection(string href) : base(href) { }
        }

        public static ResourceQuery<Callback> query()
        {
            return new ResourceQuery<Callback>(resource_href);
        }
    }
}
