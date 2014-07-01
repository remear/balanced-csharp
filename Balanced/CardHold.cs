using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Balanced
{
    public class CardHold : Resource
    {
        [JsonIgnore]
        public static string resource_href
        {
            get { return "/card_holds"; }
        }

        // fields
        public int amount { get; set; }
        public string description { get; set; }

        // attributes
        [JsonIgnore]
        public string currency { get; set; }
        [JsonIgnore]
        public DateTime expires_at { get; set; }
        [JsonIgnore]
        public string failure_reason { get; set; }
        [JsonIgnore]
        public string failure_reason_code { get; set; }
        [JsonIgnore]
        public string status { get; set; }
        [JsonIgnore]
        public string transaction_number { get; set; }
        [JsonIgnore]
        public string voided_at { get; set; }

        [JsonIgnore]
        [ResourceField(field="card_holds.card")]
        public Card card { get; set; }

        [JsonIgnore]
        [ResourceField(field = "card_holds.debit")]
        public Debit debit { get; set; }

        [JsonIgnore]
        [ResourceField(field = "card_holds.debits")]
        public Debit.Collection debits { get; set; }

        [JsonIgnore]
        [ResourceField(field="card_holds.events")]
        public Event.Collection events { get; set; }


        public CardHold() { }

        public CardHold(Dictionary<string, object> payload) { }

        public static CardHold Fetch(string href)
        {
            return Resource.Fetch<CardHold>(href);
        }

        public CardHold save()
        {
            return this.save<CardHold>();
        }

        public class Collection : ResourceCollection<CardHold>
        {
            public Collection() : base(resource_href) { }
            public Collection(string href) : base(href) { }
        }

        public static ResourceQuery<CardHold> query()
        {
            return new ResourceQuery<CardHold>(resource_href);
        }
    }
}
