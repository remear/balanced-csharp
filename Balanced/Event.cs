using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Balanced
{
    public class Event : Resource
    {
        [JsonIgnore]
        public static string resource_href
        {
            get { return "/events"; }
        }

        // attributes
        [JsonIgnore]
        public DateTime occurred_at { get; set; }
        [JsonIgnore]
        public string type { get; set; }
        [JsonIgnore]
        public Dictionary<string, int> callback_statuses { get; set; }
        [JsonIgnore]
        public Dictionary<string, object> entity { get; set; }
        [JsonIgnore]
        public Callback.Collection callbacks;

        public static Event Fetch(string href)
        {
            return Resource.Fetch<Event>(href);
        }

        public class Collection : ResourceCollection<Event>
        {
            public Collection() : base(resource_href) { }
            public Collection(string href) : base(href) { }
        }

        public static ResourceQuery<Event> query()
        {
            return new ResourceQuery<Event>(resource_href);
        }
    }
}
