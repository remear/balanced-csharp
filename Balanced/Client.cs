using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Cache;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Web;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Balanced.Exceptions;

namespace Balanced
{
    public static class Client
    {

        private static dynamic Op<T>(string path, string method, string payload)
        {
            Uri url = new Uri(Balanced.getAPIURL() + path);
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.UserAgent = "balanced-csharp/" + Balanced.getVersion();
            request.Method = method;
            request.ContentType = "application/json;revision=1.1";
            request.Accept = "application/vnd.api+json;revision=1.1";
            request.Timeout = 60000;
            request.CachePolicy = new RequestCachePolicy(RequestCacheLevel.BypassCache);
            string autorization = Balanced.API_KEY + ":";
            byte[] binaryAuthorization = System.Text.Encoding.UTF8.GetBytes(autorization);
            autorization = Convert.ToBase64String(binaryAuthorization);
            autorization = "Basic " + autorization;
            request.Headers.Add("AUTHORIZATION", autorization);

            if (!String.IsNullOrWhiteSpace(payload))
            {
                byte[] postBytes = Encoding.UTF8.GetBytes(payload);
                request.ContentLength = postBytes.Length;
                Stream dataStream = request.GetRequestStream();
                dataStream.Write(postBytes, 0, postBytes.Length);
                dataStream.Close();
            }

            string responsePayload = string.Empty;
            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    using (var stream = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(1252)))
                    {
                        responsePayload = stream.ReadToEnd();
                    }
                }
            }
            catch (WebException ex)
            {
                using (var stream = new StreamReader(ex.Response.GetResponseStream()))
                {
                    responsePayload = stream.ReadToEnd();
                }

                HttpWebResponse response = (HttpWebResponse)ex.Response;

                if ((int)response.StatusCode >= 299)
                {
                    if (responsePayload != null)
                        error(response, responsePayload);
                    else
                        throw new HTTPException(response, responsePayload);
                }
            }

            return Deserialize<T>(responsePayload);
        }

        public static dynamic Get<T>(string path, Dictionary<string, string> queryParams)
        {
            var queryString = (string)null;

            if (queryParams != null && queryParams.Count > 0)
            {
                queryString = ToQueryString(queryParams);
                path = path + queryString;
            }

            return Op<T>(path, "GET", null);
        }

        private static string ToQueryString(Dictionary<string, string> queryParams)
        {
            var array = (from key in queryParams.Keys
                         from value in queryParams.Values
                         select string.Format("{0}={1}", HttpUtility.UrlEncode(key), HttpUtility.UrlEncode(value)))
                .ToArray();
            return "?" + string.Join("&", array);
        }

        public static void error(HttpWebResponse response, string responsePayload)
        {
            if ((int)response.StatusCode == 500)
            {
                throw new HTTPException(response, responsePayload);
            }
            else
            {
                var responseObject = JObject.Parse(responsePayload.ToString());
                var error = responseObject["errors"][0];
                Dictionary<string, object> dict = JsonConvert.DeserializeObject<Dictionary<string, object>>(error.ToString());
                throw new APIException(response, dict);
            }
        }
 
        public static dynamic post<T>(string path, string payload)
        {
            return Op<T>(path, "POST", payload);
        }

        public static dynamic put<T>(string path, string payload)
        {
            return Op<T>(path, "PUT", payload);
        }

        public static dynamic Deserialize<T>(string payload)
        {
            var responseObject = JObject.Parse(payload.ToString());
            IList<string> keys = responseObject.Properties().Select(p => p.Name).ToList();

            dynamic resource = null;

            foreach (string key in keys)
            {
                // ignore links and meta
                if (key.Equals("links") || key.Equals("meta"))
                {
                    continue;
                }

                resource = responseObject[key][0].ToObject<T>();
            }
            return resource;
        }

        /*
        public void delete(string address, object body)
        {
            var request = (HttpWebRequest)WebRequest.Create(this.root + address);
            request.UserAgent = "BalancedCSharp Client Library https://github.com/rembling/BalancedPaymentsCSharp";
            request.Method = "DELETE";
            request.ContentType = "application/json";
            request.Credentials = new NetworkCredential(this.key, "");
            request.PreAuthenticate = true;
            request.Timeout = 15000;
            request.CachePolicy = new RequestCachePolicy(RequestCacheLevel.BypassCache);

            if (body != null)
            {
                request.Method = "PUT";
                var json = JsonConvert.SerializeObject(body);
                byte[] postBytes = Encoding.UTF8.GetBytes(json);
                request.ContentLength = postBytes.Length;
                Stream dataStream = request.GetRequestStream();
                dataStream.Write(postBytes, 0, postBytes.Length);
                dataStream.Close();
            }

            string output = string.Empty;
            using (var response = request.GetResponse())
            {
                using (var stream = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(1252)))
                {
                    output = stream.ReadToEnd();
                }
            }
            //for debugging
            //Console.WriteLine(output);
        }

        public void delete(string address)
        {
            delete(address, null);
        }

        /// <summary>
        /// When the DELETE verb cannot be used (i.e. when the Resource has child records, or is associated to an account or another Resource,
        /// use this method; it PUTS a meta tag of "is_valid" = "false" and basically accomplishes the same thing as a DELETE. Note, invalid or
        /// deleted Resources may still show up in lists on the BalancedPayments dashboard website
        /// </summary>
        /// <param name="address"></param>
        /// <param name="body"></param>
        public void invalidate(string uri)
        {
            var request = (HttpWebRequest)WebRequest.Create(this.root + uri);
            request.UserAgent = "BalancedCSharp Client Library https://github.com/rembling/BalancedPaymentsCSharp";
            request.Method = "PUT";
            request.ContentType = "application/json";
            request.Credentials = new NetworkCredential(this.key, "");
            request.PreAuthenticate = true;
            request.Timeout = 15000;
            request.CachePolicy = new RequestCachePolicy(RequestCacheLevel.BypassCache);

            var body = new Dictionary<string, string>();
            body.Add("is_valid", "false");

            if (body != null)
            {
                var json = JsonConvert.SerializeObject(body);
                byte[] postBytes = Encoding.UTF8.GetBytes(json);
                request.ContentLength = postBytes.Length;
                Stream dataStream = request.GetRequestStream();
                dataStream.Write(postBytes, 0, postBytes.Length);
                dataStream.Close();
            }

            string output = string.Empty;
            using (var response = request.GetResponse())
            {
                using (var stream = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(1252)))
                {
                    output = stream.ReadToEnd();
                }
            }
            //for debugging
            //Console.WriteLine(output);
        }

        private Uri buildUri(String path, Dictionary<string, string> prams)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(root);
            sb.Append(path);
            if (prams != null && prams.Count() > 0)
            {
                sb.Append("?");
                sb.Append(buildQueryString(prams));
            }
            return new Uri(sb.ToString());
        }
        private string buildQueryString(Dictionary<string, string> prams)
        {
            StringBuilder queryString = new StringBuilder();
            foreach (var s in prams)
            {
                queryString.Append(string.Format("{0}={1}&", s.Key, s.Value));
            }
            return queryString.ToString();
        }*/
    }
}
