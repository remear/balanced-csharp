using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Balanced
{
    public class BankAccount : FundingInstrument
    {
        [JsonIgnore]
        public static string resource_href
        {
            get { return "/bank_accounts"; }
        }

        // fields
        public string account_type { get; set; }
        public string account_number { get; set; }
        public Dictionary<string, string> address { get; set; }
        public string name { get; set; }
        public string routing_number { get; set; }

        // attributes
        [JsonIgnore]
        [ResourceField(field = "bank_accounts.bank_account_verifications")]
        public BankAccountVerification.Collection verifications { get; set; }

        [JsonIgnore]
        [ResourceField(field = "bank_accounts.bank_account_verification")]
        public BankAccountVerification verification { get; set; }

        [JsonIgnore]
        [ResourceField(field = "bank_accounts.credits")]
        public Credit.Collection credits { get; set; }

        [JsonIgnore]
        [ResourceField(field = "bank_accounts.debits")]
        public Debit.Collection debits { get; set; }


        public BankAccount() { }

        public BankAccount(Dictionary<string, object> payload) { }

        public static BankAccount Fetch(string href)
        {
            return Resource.Fetch<BankAccount>(href, null);
        }

        public static BankAccount Fetch(string href, Dictionary<string, string> queryParams)
        {
            return Resource.Fetch<BankAccount>(href, queryParams);
        }

        public BankAccount save()
        {
            return this.save<BankAccount>();
        }

        public class Collection : ResourceCollection<Card>
        {
            public Collection() : base(resource_href) { }
            public Collection(string href) : base(href) { }
        }
    }
}
