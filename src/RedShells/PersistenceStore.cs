using RedShells.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedShells
{

    [Export(typeof(IPersistenceStore))]
    public class PersistenceStore : IPersistenceStore
    {
        protected Dictionary<string, object> Store { get; set; }

        public PersistenceStore()
        {
            Store = new Dictionary<string, object>();
        }

        public T GetValue<T>(string key)
        {
            object result = null;

            if (Store.TryGetValue(key, out result))
            {
                return (T)result;
            }

            return default(T);
        }

        public void Add<T>(string key, T value)
        {
            if (Store.ContainsKey(key))
            {
                Store[key] = value;
            }
            else
            {
                Store.Add(key, value);
            }
        }
    }
}
