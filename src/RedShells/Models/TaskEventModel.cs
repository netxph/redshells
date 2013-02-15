using RedShells.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RedShells.Models
{
    public class TaskEventModel
    {

        public IShellContext Shell { get; set; }

        public List<ITaskEventListener> Listeners { get; set; }

        public void Listen(string handlerName, string handlerParameters, string taskCommand)
        {
            if (Listeners != null)
            {
                var listener = Listeners.FirstOrDefault(l => l.Name == handlerName);

                if (listener != null)
                {
                    listener.OnEventTriggered += (sender, e) =>
                    {
                        Shell.ShellInvoke(taskCommand);
                    };

                    listener.Listen();
                }
                else
                {
                    throw new Exception(string.Format("Listener [{0}] is missing", handlerName));
                }
            }
            
        }
        
    }
}
