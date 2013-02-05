using RedShells.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace RedShells.Commands
{

    [Cmdlet(VerbsCommon.Remove, "Workspace")]
    public class RemoveWorkspaceCommand : BaseCommand<WorkspaceModel>
    {

        [Parameter(Position = 0, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string Key { get; set; }

        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            Model.Remove(Key);
        }

    }
}
