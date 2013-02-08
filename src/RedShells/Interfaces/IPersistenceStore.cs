using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedShells.Interfaces
{
    public interface IPersistenceStore
    {
        T GetValue<T>(string key);

        void Add<T>(string key, T value);
    }
}
