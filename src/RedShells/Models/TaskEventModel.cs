using RedShells.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RedShells.Models
{
    public class TaskEventModel
    {

        [Import]
        public IShellContext Shell { get; set; }

        [ImportMany]
        public List<ITaskEventListener> Listeners { get; set; }

        public void Listen(string handlerName, string handlerParameters, string taskCommand)
        {
            if (Listeners != null)
            {
                var listener = Listeners.FirstOrDefault(l => l.Name == handlerName);

                if (listener != null)
                {
                    listener.EventTriggered += (sender, e) =>
                    {
                        Shell.ShellInvoke(taskCommand);
                    };

                    listener.Listen(handlerParameters);
                }
                else
                {
                    throw new Exception(string.Format("Listener [{0}] is missing", handlerName));
                }
            }
            
        }
        
    }
}
