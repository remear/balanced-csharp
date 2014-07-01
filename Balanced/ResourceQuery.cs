using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Balanced
{
    public class ResourceQuery<T> : ResourcePagination<T>
    {
        string dateFormat = "{yyyy'-'MM'-'dd'T'HH':'mm':'ss.fff'Z'}";

        public ResourceQuery(string href) : base(href) { }

        public dynamic first()
        {
            base.getURIBuilder().SetQueryParam("limit", "1");
            List<T> items = all();
            if (items.Count() == 0)
                return null;
            return items[0];
        }


        // filtering

        public ResourceQuery<T> filter(String field, String op, String value)
        {
            String name = String.Format("%s[%s]", field, op);
            base.getURIBuilder().SetQueryParam(name, value);
            return this;
        }

        public ResourceQuery<T> filter(String field, String op, String[] values)
        {
            String name = String.Format("%s[%s]", field, op);
            String value = String.Join(",", values);
            base.getURIBuilder().SetQueryParam(name, value);
            return this;
        }

        public ResourceQuery<T> filter(String field, String value)
        {
            base.getURIBuilder().SetQueryParam(field, value);
            return this;
        }

        public ResourceQuery<T> filter(String field, String[] values)
        {
            String value = String.Join(",", values);
            base.getURIBuilder().SetQueryParam(field, value);
            return this;
        }

        public ResourceQuery<T> filter(String field, String op, DateTime value)
        {
            return this.filter(field, op, String.Format(dateFormat, value));
        }

        public ResourceQuery<T> filter(String field, String op, DateTime[] values)
        {
            String[] transformed = new String[values.Length];
            for (int i = 0; i != values.Length; i++)
                transformed[i] = String.Format(dateFormat, values[i]);
            return this.filter(field, op, transformed);
        }

        public ResourceQuery<T> filter(String field, DateTime value)
        {
            return this.filter(field, String.Format(dateFormat, value));
        }

        public ResourceQuery<T> filter(String field, DateTime[] values)
        {
            String[] transformed = new String[values.Length];
            for (int i = 0; i != values.Length; i++)
                transformed[i] = String.Format(dateFormat, values[i]);
            return this.filter(field, transformed);
        }

        public ResourceQuery<T> filter(String field, String op, Object value)
        {
            return this.filter(field, op, value.ToString());
        }

        public ResourceQuery<T> filter(String field, String op, Object[] values)
        {
            String[] transformed = new String[values.Length];
            for (int i = 0; i != values.Length; i++)
                transformed[i] = values[i].ToString();
            return this.filter(field, op, transformed);
        }

        public ResourceQuery<T> filter(String field, Object value)
        {
            return this.filter(field, value.ToString());
        }

        public ResourceQuery<T> filter(String field, Object[] values)
        {
            String[] transformed = new String[values.Length];
            for (int i = 0; i != values.Length; i++)
                transformed[i] = values[i].ToString();
            return this.filter(field, transformed);
        }

        // sorting

        public ResourceQuery<T> order_by(String field)
        {
            base.getURIBuilder().SetQueryParam("sort", field);
            return this;
        }

        public ResourceQuery<T> order_by(String field, String order)
        {
            String value = String.Format("%s,%s", field, order);
            base.getURIBuilder().SetQueryParam("sort", value);
            return this;
        }
    }
}
