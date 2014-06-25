using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Balanced
{
    public class ResourcePage<T>
    {
        //private Client client = Balanced.getInstance().getClient();
    
        public string href;
        private List<T> items;
        private int total;
        private string first_uri;
        private string previous_uri;
        private string next_uri;
        private string last_uri;
    
        public ResourcePage(
                string pHref,
                List<T> pItems,
                int pTotal,
                string pFirst_uri,
                string pPrevious_uri,
                string pNext_uri,
                string pLast_uri)
        {
            /*href = href;
            items = items;
            total = total;        
            first_uri = first_uri;
            previous_uri = previous_uri;
            next_uri = next_uri;
            last_uri = last_uri;*/
        
        }
    
        public ResourcePage(string pHref)
        {
            href = pHref;
        }
    
        public int getSize()
        {
            return getItems().Count();
        }
    
        public List<T> getItems()
        {
            if (items == null) 
                load();
            return items;
        }
    
        public string getFirstUri()
        {
            if (items == null) 
                load();
            return first_uri;
        }
    
        public ResourcePage<T> getFirst()
        {
            return new ResourcePage<T>(getFirstUri());
        }
    
        public string getPreviousUri()
        {
            if (items == null) 
                load();
            return previous_uri;
        }
    
        public ResourcePage<T> getPrevious()
        {
            return new ResourcePage<T>(getPreviousUri());
        }
    
        public string getNextUri()
        {
            if (items == null) 
                load();
            return next_uri;
        }
    
        public ResourcePage<T> getNext()
        {
            return new ResourcePage<T>(getNextUri());
        }
    
        public String getLastUri()
        {
            return last_uri;
        }
    
        public ResourcePage<T> getLast()
        {
            return new ResourcePage<T>(getLastUri());
        }

    
        public int getTotal()
        {
            if (items == null) 
                load();
            return total;
        }
    
        private void load()
        {
            string responsePayload = Client.Get<Dictionary<string, object>>(href, null, false);
            var responseObject = JObject.Parse(responsePayload);
            IList<string> keys = responseObject.Properties().Select(p => p.Name).ToList();

            Dictionary<string, object> meta = responseObject["meta"].ToObject<Dictionary<string, object>>();
            Dictionary<string, string> hyperlinks = responseObject["links"].ToObject<Dictionary<string, string>>();
            items = new List<T>();


            foreach (string key in keys)
            {
                // ignore links and meta
                if (key.Equals("links") || key.Equals("meta"))
                    continue;

                List<JObject> objs = responseObject[key].ToObject<List<JObject>>();
                foreach (JObject o in objs)
                {
                    var cust = o.ToObject<T>();
                    items.Add(cust);
                }
                //resources = ;
            }


            //JObject metaObj = (JObject)page["meta"];
            //Dictionary<string, object> meta = metaObj.ToObject<Dictionary<string, object>>();
            href = (string)meta["href"];
            total = Convert.ToInt32(meta["total"]);
            first_uri = (string)meta["first"];
            last_uri = (string)meta["last"];
            previous_uri = (string)meta["previous"];
            next_uri = (string)meta["next"];

            /*List<Dictionary<string, object>> objs = (List<Dictionary<string, object>>) page.get(Utils.classNameToResourceKey(cls.getSimpleName()));
            if (objs != null) {
                items = new ArrayList<T>(objs.size());
                try {
                    for (Map<String, Object> obj: objs) {
                        T t = cls.newInstance();
                        ((Resource) t).hydrate(hyperlinks, resourceMeta, obj);
                        ((Resource) t).constructFromResponse(obj);
                        items.add(t);
                    }
                }
                catch (InstantiationException e) {
                    throw new RuntimeException(e);
                }
                catch (IllegalAccessException e) {
                    throw new RuntimeException(e);
                }
            }*/
        }
    }
}
