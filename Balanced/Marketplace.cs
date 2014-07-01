using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Balanced
{
    public class Marketplace : Resource
    {
        public static string resource_href
        {
            get { return "/marketplaces"; }
        }

        // fields
        public string domain_url { get; set; }
        public string name { get; set; }
        public string support_email_address { get; set; }
        public string support_phone_number { get; set; }

        // attributes
        [JsonIgnore]
        public int in_escrow { get; set; }

        [JsonIgnore]
        [ResourceField(field = "marketplaces.bank_accounts")]
        public BankAccount.Collection bank_accounts { get; set; }

        [JsonIgnore]
        [ResourceField(field = "marketplaces.callbacks")]
        public Callback.Collection callbacks { get; set; }

        [JsonIgnore]
        [ResourceField(field = "marketplaces.cards")]
        public Card.Collection cards { get; set; }

        [JsonIgnore]
        [ResourceField(field = "marketplaces.customers")]
        public Customer.Collection customers { get; set; }

        [JsonIgnore]
        [ResourceField(field = "marketplaces.credits")]
        public Credit.Collection credits { get; set; }

        [JsonIgnore]
        [ResourceField(field = "marketplaces.card_holds")]
        public CardHold.Collection card_holds { get; set; }

        [JsonIgnore]
        [ResourceField(field = "marketplaces.debits")]
        public Debit.Collection debits { get; set; }

        [JsonIgnore]
        [ResourceField(field = "marketplaces.events")]
        public Event.Collection events { get; set; }

        [JsonIgnore]
        [ResourceField(field = "marketplaces.refunds")]
        public Refund.Collection refunds { get; set; }

        [JsonIgnore]
        [ResourceField(field = "marketplaces.reversals")]
        public Reversal.Collection reversals { get; set; }

        [JsonIgnore]
        [ResourceField(field = "marketplaces.owner_customer")]
        public Customer owner_customer { get; set; }

        [JsonIgnore]
        public bool production { get; set; }

        [JsonIgnore]
        public int unsettled_fees { get; set; }


        public static Marketplace Mine()
        {
            Marketplace mp = Marketplace.query().first();
            if (mp == null)
                throw new SystemException("A Marketplace is required but was not found");
            return mp;
        }

        public Marketplace() { }

        public Marketplace(Dictionary<string, object> payload) { }

        public static Marketplace Fetch(string href)
        {
            return Resource.Fetch<Marketplace>(href);
        }

        public Marketplace save()
        {
            return this.save<Marketplace>();
        }

        public static ResourceQuery<Marketplace> query()
        {
            return new ResourceQuery<Marketplace>(resource_href);
        }
    }
}
