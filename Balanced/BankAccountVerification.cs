using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Balanced
{
    public class BankAccountVerification : Resource
    {
        [JsonIgnore]
        public static string resource_href
        {
            get { return "/bank_account_verifications"; }
        }

        // fields
        public int attempts { get; set; }
        public int attempts_remaining { get; set; }
        public string deposit_status { get; set; }
        public string verification_status { get; set; }

        // attributes

        [JsonIgnore]
        [ResourceField(field = "bank_account_verifications.bank_account")]
        public BankAccount bank_account { get; set; }


        public BankAccountVerification() { }

        public BankAccountVerification(Dictionary<string, object> payload) { }

        public static BankAccountVerification Fetch(string href)
        {
            return Resource.Fetch<BankAccountVerification>(href);
        }

        public BankAccountVerification save()
        {
            return this.save<BankAccountVerification>();
        }

        public class Collection : ResourceCollection<BankAccountVerification>
        {
            public Collection() : base(resource_href) { }
            public Collection(string href) : base(href) { }
        }

        public static ResourceQuery<BankAccountVerification> query()
        {
            return new ResourceQuery<BankAccountVerification>(resource_href);
        }
    }
}
