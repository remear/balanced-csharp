using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Balanced
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ResourceField : Attribute
    {
        string _field;

        public string field
        {
            get { return _field; }
            set { _field = value; }
        }

        public ResourceField()
        {

        }
    }
}
