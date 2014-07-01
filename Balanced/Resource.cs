using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

using Newtonsoft.Json;
using System.Dynamic;
using System.Reflection;

namespace Balanced
{
    public abstract class Resource
    {
        public string href { get; set; }
        [JsonIgnore]
        public Dictionary<string, string> hyperlinks { get; set; }
        public string id { get; set; }
        public Dictionary<string, string> links { get; set; }
        public Dictionary<string, string> meta { get; set; }
        [JsonIgnore]
        public DateTime created_at { get; set; }
        [JsonIgnore]
        public DateTime updated_at { get; set; }

        public Resource() { }

        public T save<T>()
        {
            if (this.href != null)
            {
                return Client.put<T>(this.href, serialize(this));
            }
            else
            {
                string href = this.GetType().GetProperty("resource_href").GetValue(this).ToString();
                return Client.post<T>(href, serialize(this));
            }
        }

        public static T Fetch<T>(string href)
        {
            return Client.Get<T>(href, null);
        }

        public static string serialize(object resource)
        {
            return JsonConvert.SerializeObject(resource,
                new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }
    }
}
