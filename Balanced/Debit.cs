﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Balanced
{
    public class Debit : Resource
    {
        [JsonIgnore]
        public static string resource_href
        {
            get { return "/debits"; }
        }

        // fields
        public int amount { get; set; }
        public string appears_on_statement_as { get; set; }
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
        [ResourceField(field = "debits.customer")]
        public Customer customer { get; set; }

        [JsonIgnore]
        [ResourceField(field = "debits.dispute")]
        public Dispute dispute { get; set; }

        [JsonIgnore]
        [ResourceField(field = "debits.events")]
        public Event.Collection events { get; set; }

        [JsonIgnore]
        [ResourceField(field = "debits.refunds")]
        public Refund.Collection refunds { get; set; }

        [JsonIgnore]
        [ResourceField(field = "debits.source")]
        public FundingInstrument source { get; set; }


        public Debit() { }

        public Debit(Dictionary<string, object> payload) { }

        public static Debit Fetch(string href)
        {
            return Resource.Fetch<Debit>(href);
        }

        public Debit save()
        {
            return this.save<Debit>();
        }

        public class Collection : ResourceCollection<Debit>
        {
            public Collection() : base(resource_href) { }
            public Collection(string href) : base(href) { }
        }

        public static ResourceQuery<Debit> query()
        {
            return new ResourceQuery<Debit>(resource_href);
        }
    }
}
