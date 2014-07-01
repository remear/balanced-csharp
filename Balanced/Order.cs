using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Balanced
{
    public class Order : Resource
    {
        [JsonIgnore]
        public static string resource_href
        {
            get { return "/orders"; }
        }

        // fields
        public string description { get; set; }
        public Dictionary<string, string> delivery_address { get; set; }
        
        // attributes
        [JsonIgnore]
        public int amount;
        [JsonIgnore]
        public int amount_escrowed;
        [JsonIgnore]
        public string currency { get; set; }

        [JsonIgnore]
        [ResourceField(field = "orders.buyers")]
        public Customer.Collection customers { get; set; }

        [JsonIgnore]
        [ResourceField(field = "orders.credits")]
        public Credit.Collection credits { get; set; }

        [JsonIgnore]
        [ResourceField(field = "orders.debits")]
        public Debit.Collection debits { get; set; }

        [JsonIgnore]
        [ResourceField(field = "orders.merchant")]
        public Customer merchant { get; set; }

        [JsonIgnore]
        [ResourceField(field = "orders.refunds")]
        public Refund.Collection refunds { get; set; }

        [JsonIgnore]
        [ResourceField(field = "orders.reversals")]
        public Refund.Collection reversals { get; set; }


        public Order() { }

        public Order(Dictionary<string, object> payload) { }

        public static Order Fetch(string href)
        {
            return Resource.Fetch<Order>(href);
        }

        public Order save()
        {
            return this.save<Order>();
        }

        public class Collection : ResourceCollection<Order>
        {
            public Collection() : base(resource_href) { }
            public Collection(string href) : base(href) { }
        }

        public static ResourceQuery<Order> query()
        {
            return new ResourceQuery<Order>(resource_href);
        }
    }
}
