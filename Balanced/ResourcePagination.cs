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

        public ResourcePagination(string pHref)
        {
            href = pHref;
        }

        public IEnumerator GetEnumerator()
        {
            ResourcePage<T> page = new ResourcePage<T>(href);
            return new ResourceIterator<T>(href, page);
        }


        public class ResourceIterator<T> : IEnumerator
        {
            public String href;
            public ResourcePage<T> page;
            public int index;

            public ResourceIterator(String href, ResourcePage<T> page)
            {
                this.href = href;
                this.page = page;
                this.index = 0;
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
        }
    }
}
