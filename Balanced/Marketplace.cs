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
        [JsonIgnore]
        public static string resource_href
        {
            get { return "/marketplaces"; }
        }

        // fields
        [ResourceField]
        public string domain_url { get; set; }
        [ResourceField]
        public string name { get; set; }
        [ResourceField]
        public string support_email_address { get; set; }
        [ResourceField]
        public string support_phone_number { get; set; }

        // attributes
        [ResourceField(serialize = false)]
        public int in_escrow { get; set; }

        [ResourceField(field = "marketplaces.bank_accounts", serialize = false)]
        public BankAccount.Collection bank_accounts { get; set; }

        [ResourceField(field = "marketplaces.callbacks", serialize = false)]
        public Callback.Collection callbacks { get; set; }

        [ResourceField(field = "marketplaces.cards", serialize = false)]
        public Card.Collection cards { get; set; }

        [ResourceField(field = "marketplaces.customers", serialize = false)]
        public Customer.Collection customers { get; set; }

        [ResourceField(field = "marketplaces.credits", serialize = false)]
        public Credit.Collection credits { get; set; }

        [ResourceField(field = "marketplaces.card_holds", serialize = false)]
        public CardHold.Collection card_holds { get; set; }

        [ResourceField(field = "marketplaces.debits", serialize = false)]
        public Debit.Collection debits { get; set; }

        [ResourceField(field = "marketplaces.events", serialize = false)]
        public Event.Collection events { get; set; }

        [ResourceField(field = "marketplaces.refunds", serialize = false)]
        public Refund.Collection refunds { get; set; }

        [ResourceField(field = "marketplaces.reversals", serialize = false)]
        public Reversal.Collection reversals { get; set; }

        [ResourceField(field = "marketplaces.owner_customer", serialize = false)]
        public Customer owner_customer { get; set; }

        [ResourceField(serialize = false)]
        public bool production { get; set; }

        [ResourceField(serialize = false)]
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

        public void save()
        {
            this.save<Marketplace>();
        }

        public static ResourceQuery<Marketplace> query()
        {
            return new ResourceQuery<Marketplace>(resource_href);
        }
    }
}
