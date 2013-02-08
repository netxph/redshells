using RedShells.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace RedShells.Commands
{
    [Cmdlet(VerbsCommon.Set, "Workspace")]
    public class SetWorkspaceCommand : BaseCommand<WorkspaceModel>
    {

        [Parameter(Position = 0, Mandatory = true, ParameterSetName="Normal")]
        [ValidateNotNullOrEmpty]
        public string Key { get; set; }

        [Parameter(Mandatory=true, ParameterSetName = "History")]
        public SwitchParameter Back { get; set; }

        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            if (!Back)
            {
                Model.Set(Key);
            }
            else
            {
                Model.MoveBack();
            }
        }

    }
}
