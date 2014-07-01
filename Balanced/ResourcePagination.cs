using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Balanced
{
    public class ResourcePagination<T> : IEnumerable
    {
        private string href;
        private ResourceIterator iterator;
        private UriBuilder uri_builder;

        protected UriBuilder getURIBuilder()
        {
            return uri_builder;
        }

        public ResourcePagination(string pHref)
        {
            href = pHref;
            uri_builder = new UriBuilder();
            uri_builder.Path = href;
        }

        public IEnumerator GetEnumerator()
        {
            ResourcePage<T> page = new ResourcePage<T>(href);
            iterator = new ResourceIterator(href, page);
            return iterator;
        }

        public int Total()
        {
            return iterator.Total();
        }

        protected string getURI()
        {
            return uri_builder.Path.ToString() + uri_builder.Query.ToString();
        }

        public List<T> all()
        {
            string href = getURI();
            ResourcePage<T> page = new ResourcePage<T>(href);
            List<T> items = new List<T>(page.getTotal());
            ResourceIterator iterator = new ResourceIterator(href, page);
            while (iterator.MoveNext())
            {
                object obj = iterator.Current;
                items.Add((T)obj);
            }
            return items;
        }

        public class ResourceIterator : IEnumerator
        {
            private String href;
            private ResourcePage<T> page;
            private int index;
            private int total;

            public ResourceIterator(String pHref, ResourcePage<T> pPage)
            {
                href = pHref;
                page = pPage;
                index = 0;
                total = page.getTotal();
            }

            public object Current
            {
                get
                {
                    if (index >= page.getSize())
                    {
                        page = page.getNext();
                        index = 0;
                    }
                    T t = page.getItems()[index];
                    index += 1;
                    return t;
                }
            }

            public bool MoveNext()
            {
                return (index < page.getSize() || this.page.getNextUri() != null);
            }

            public int Total()
            {
                return total;
            }

            public void Reset()
            {
                throw new NotImplementedException();
            }
        }
    }
}
