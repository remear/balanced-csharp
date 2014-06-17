using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Balanced.Exceptions
{
    public class APIException : HTTPException
    {
        public string category_type { get; set; }
        public string category_code { get; set; }
        public string description { get; set; }
        public Dictionary<string, object> extras { get; set; }
        public string request_id { get; set; }
        //public string status;
        //public int status_code;

        public APIException() { }

        public APIException(
            HttpWebResponse response,
            Dictionary<string, object>responsePayload)
        {
            IList<PropertyInfo> props = new List<PropertyInfo>(this.GetType().GetProperties());

            foreach (PropertyInfo prop in props)
            {
                if (responsePayload.ContainsKey(prop.Name))
                {
                    prop.SetValue(this, responsePayload[prop.Name]);
                }
                
                //object propValue = prop.GetValue(this, null);

                
            }

            /*category_type = responsePayload.ContainsKey("category_type") ? (string)responsePayload["category_type"] : null;
            category_code = responsePayload.ContainsKey("category_code") ? (string)responsePayload["category_code"] : null;
            description = responsePayload.ContainsKey("description") ? (string)responsePayload["description"] : null;
            request_id = responsePayload.ContainsKey("description") ? (string)responsePayload["request_id"] : null;
            status = (string)responsePayload["status"];
            status_code = (int)responsePayload["status_code"];*/
        }
    }
}
