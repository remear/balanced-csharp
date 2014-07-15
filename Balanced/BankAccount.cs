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
        [ResourceField]
        public string account_type { get; set; }
        [ResourceField]
        public string account_number { get; set; }
        [ResourceField]
        public Dictionary<string, string> address { get; set; }
        [ResourceField]
        public string name { get; set; }
        [ResourceField]
        public string routing_number { get; set; }

        // attributes
        [ResourceField(field = "bank_accounts.bank_account_verifications", link = true, serialize = false)]
        public BankAccountVerification.Collection verifications { get; set; }

        [ResourceField(field = "bank_accounts.bank_account_verification", link = true, serialize = false)]
        public BankAccountVerification verification { get; set; }

        [ResourceField(field = "bank_accounts.credits", link = true, serialize = false)]
        public Credit.Collection credits { get; set; }

        [ResourceField(field = "bank_accounts.debits", link = true, serialize = false)]
        public Debit.Collection debits { get; set; }


        public BankAccount() { }

        public BankAccount(Dictionary<string, object> payload) { }

        public static BankAccount Fetch(string href)
        {
            return Resource.Fetch<BankAccount>(href);
        }

        public void save()
        {
            this.save<BankAccount>();
        }

        public void reload()
        {
            this.reload<BankAccount>();
        }

        public class Collection : ResourceCollection<BankAccount>
        {
            public Collection() : base(resource_href) { }
            public Collection(string href) : base(href) { }
        }

        public static ResourceQuery<BankAccount> query()
        {
            return new ResourceQuery<BankAccount>(resource_href);
        }

        public BankAccountVerification verify() { return verifications.create(); }
    }
}
