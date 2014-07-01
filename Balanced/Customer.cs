using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Balanced
{
    public class Customer : Resource
    {
        [JsonIgnore]
        public static string resource_href
        {
            get { return "/customers"; }
        }

        public Dictionary<string, string> address { get; set; }
        public string business_name { get; set; }
        public int? dob_month { get; set; }
        public int? dob_year { get; set; }
        public string ein { get; set; }
        public string email { get; set; }
        public string name { get; set; }
        public string phone { get; set; }
        public string source { get; set; }
        public string ssn_last4 { get; set; }

        [JsonIgnore]
        [ResourceField(field="customers.credits")]
        public Credit.Collection credits { get; set; }

        [JsonIgnore]
        [ResourceField(field = "customers.debits")]
        public Debit.Collection debits { get; set; }


        public Customer() { }

        public Customer(Dictionary<string, object> payload) { }

        public static Customer Fetch(string href)
        {
            return Resource.Fetch<Customer>(href);
        }

        public Customer save()
        {
            return this.save<Customer>();
        }

        public class Collection : ResourceCollection<Customer>
        {
            public Collection() : base(resource_href) { }
            public Collection(string href) : base(href) { }
        }

        public static ResourceQuery<Customer> query()
        {
            return new ResourceQuery<Customer>(resource_href);
        }
    }
}
