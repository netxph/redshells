using RedShells.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace RedShells.Commands
{

    [Cmdlet(VerbsLifecycle.Start, "TaskEvent")]
    public class StartTaskEventCommand : BaseCommand<TaskEventModel>
    {

        [Parameter(Position = 0, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string Listener { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string ListenerArgs { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string Command { get; set; }

        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            Model.Listen(Listener, ListenerArgs, Command);
        }

    }
}
