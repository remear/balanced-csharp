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
        private ResourceIterator<T> iterator;

        public ResourcePagination(string pHref)
        {
            href = pHref;
        }

        public IEnumerator GetEnumerator()
        {
            ResourcePage<T> page = new ResourcePage<T>(href);
            iterator = new ResourceIterator<T>(href, page);
            return iterator;
        }

        public int Total()
        {
            return iterator.Total();
        }

        public class ResourceIterator<T> : IEnumerator
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

            public void Reset()
            {
                throw new NotImplementedException();
            }

            public int Total()
            {
                return total;
            }
        }
    }
}
