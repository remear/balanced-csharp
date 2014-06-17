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
        public string href;
        [JsonIgnore]
        public Dictionary<string, string> hyperlinks;
        public string id;
        public Dictionary<string, string> links;
        public Dictionary<string, string> meta;
        [JsonIgnore]
        public DateTime created_at;
        [JsonIgnore]
        public DateTime updated_at;

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

        public static T Fetch<T>(string href, Dictionary<string, string> queryParams)
        {
            return Client.Get<T>(href, queryParams);
        }

        public static string serialize(object resource)
        {
            string payload = JsonConvert.SerializeObject(resource, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            //string payload = JsonConvert.SerializeObject(resource);
            /*IList<PropertyInfo> props = new List<PropertyInfo>(this.GetType().GetProperties());

            Dictionary<string, object> payload;
            if (props.Count > 0)
            {

            }

            foreach (PropertyInfo prop in props)
            {
                object propValue = prop.GetValue(this, null);

                // Do something with propValue
            }
            */
            return payload;
        }
    }
}
