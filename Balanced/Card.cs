using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Balanced
{
    public class Card : FundingInstrument
    {
        [JsonIgnore]
        public static string resource_href
        {
            get { return "/cards"; }
        }

        // fields
        public Dictionary<string, string> address { get; set; }
        public string cvv { get; set; }
        public int expiration_month { get; set; }
        public int expiration_year { get; set; }
        public string name { get; set; }
        public string number { get; set; }

        // attributes
        [JsonIgnore]
        public string avs_postal_match { get; set; }
        [JsonIgnore]
        public string avs_result { get; set; }
        [JsonIgnore]
        public string avs_result_match { get; set; }
        [JsonIgnore]
        public string brand { get; set; }
        [JsonIgnore]
        public string category { get; set; }
        [JsonIgnore]
        public string cvv_match { get; set; }
        [JsonIgnore]
        public string cvv_result { get; set; }
        [JsonIgnore]
        public bool is_verified { get; set; }
        [JsonIgnore]
        public string type { get; set; }

        [JsonIgnore]
        [ResourceField(field="cards.card_holds")]
        public CardHold.Collection card_holds { get; set; }

        [JsonIgnore]
        [ResourceField(field = "cards.customer")]
        public Customer customer { get; set; }

        [JsonIgnore]
        [ResourceField(field = "cards.debits")]
        public Debit.Collection debits { get; set; }

        [JsonIgnore]
        [ResourceField(field = "cards.credits")]
        public Credit.Collection credits { get; set; }


        public Card() { }

        public Card(Dictionary<string, object> payload) { }

        public static Card Fetch(string href)
        {
            return Resource.Fetch<Card>(href, null);
        }

        public static Card Fetch(string href, Dictionary<string, string> queryParams)
        {
            return Resource.Fetch<Card>(href, queryParams);
        }

        public Card save()
        {
            return this.save<Card>();
        }

        public class Collection : ResourceCollection<Card>
        {
            public Collection() : base(resource_href) { }
            public Collection(string href) : base(href) { }
        }
    }
}
