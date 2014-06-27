using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Balanced
{
    public abstract class FundingInstrument : Resource
    {
        // attributes
        [JsonIgnore]
        public bool can_credit { get; set; }
        [JsonIgnore]
        public bool can_debit { get; set; }
        [JsonIgnore]
        public string bank_name { get; set; }
        [JsonIgnore]
        public string fingerprint { get; set; }
    }
}
