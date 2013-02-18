using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedShells.Interfaces
{
    public interface ITaskEventListener
    {
        string Name { get; set; }

        void Listen(string args);
        
        event EventHandler EventTriggered;
    }
}
