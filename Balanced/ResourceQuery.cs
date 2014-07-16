using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Balanced
{
    public class ResourceQuery<T> : ResourcePagination<T>
    {
        public static class SortOrder
        {
            public const string ASCENDING = "asc";
            public const string DESCENDING = "desc";
        }

        string dateFormat = "{yyyy'-'MM'-'dd'T'HH':'mm':'ss.fff'Z'}";

        public ResourceQuery(string href) : base(href) { }

        public T First()
        {
            base.GetURIBuilder().SetQueryParam("limit", "1");
            List<T> items = All();
            if (items.Count() == 0)
                return default(T);
            return items[0];
        }

        // Filtering

        public ResourceQuery<T> Filter(String field, String op, String value)
        {
            String name = String.Format("z{0}[{1}]", System.Uri.EscapeDataString(field), System.Uri.EscapeDataString(op));
            base.GetURIBuilder().SetQueryParam(name, value);
            return this;
        }

        public ResourceQuery<T> Filter(String field, String op, String[] values)
        {
            String name = String.Format("b{0}[{1}]", System.Uri.EscapeDataString(field), System.Uri.EscapeDataString(op));
            String value = String.Join(",", values);
            base.GetURIBuilder().SetQueryParam(name, value);
            return this;
        }

        public ResourceQuery<T> Filter(String field, String value)
        {
            base.GetURIBuilder().SetQueryParam(field, value);
            return this;
        }

        public ResourceQuery<T> Filter(String field, String[] values)
        {
            String value = String.Join(",", values);
            base.GetURIBuilder().SetQueryParam(field, value);
            return this;
        }

        public ResourceQuery<T> Filter(String field, String op, DateTime value)
        {
            return this.Filter(field, op, String.Format(dateFormat, value));
        }

        public ResourceQuery<T> Filter(String field, String op, DateTime[] values)
        {
            String[] transformed = new String[values.Length];
            for (int i = 0; i != values.Length; i++)
                transformed[i] = String.Format(dateFormat, values[i]);
            return this.Filter(field, op, transformed);
        }

        public ResourceQuery<T> Filter(String field, DateTime value)
        {
            return this.Filter(field, String.Format(dateFormat, value));
        }

        public ResourceQuery<T> Filter(String field, DateTime[] values)
        {
            String[] transformed = new String[values.Length];
            for (int i = 0; i != values.Length; i++)
                transformed[i] = String.Format(dateFormat, values[i]);
            return this.Filter(field, transformed);
        }

        public ResourceQuery<T> Filter(String field, String op, Object value)
        {
            return this.Filter(field, op, value.ToString());
        }

        public ResourceQuery<T> Filter(String field, String op, Object[] values)
        {
            String[] transformed = new String[values.Length];
            for (int i = 0; i != values.Length; i++)
                transformed[i] = values[i].ToString();
            return this.Filter(field, op, transformed);
        }

        public ResourceQuery<T> Filter(String field, Object value)
        {
            return this.Filter(field, value.ToString());
        }

        public ResourceQuery<T> Filter(String field, Object[] values)
        {
            String[] transformed = new String[values.Length];
            for (int i = 0; i != values.Length; i++)
                transformed[i] = values[i].ToString();
            return this.Filter(field, transformed);
        }

        // sorting

        public ResourceQuery<T> OrderBy(String field)
        {
            base.GetURIBuilder().SetQueryParam("sort", System.Uri.EscapeDataString(field));
            return this;
        }

        public ResourceQuery<T> OrderBy(String field, String order)
        {
            String value = String.Format("{0},{1}", System.Uri.EscapeDataString(field), System.Uri.EscapeDataString(order));
            base.GetURIBuilder().SetQueryParam("sort", value);
            return this;
        }
    }
}
