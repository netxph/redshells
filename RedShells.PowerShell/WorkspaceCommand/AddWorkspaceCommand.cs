using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace RedShells.PowerShell.WorkspaceCommand
{
    [Cmdlet(VerbsCommon.Add, "Workspace")]
    public class AddWorkspaceCommand : PSCmdlet, IWorkspaceView
    {
        public AddWorkspaceCommand()
        {
            Controller = new WorkspaceController(this);
        }

        protected WorkspaceController Controller { get; set; }

        protected override void ProcessRecord()
        {
            Controller.SaveWorkspace(Key, SessionState.Path.CurrentLocation.Path);
        }

        public void WriteObject(object @object)
        {
            WriteObject(@object);
        }

        [ValidateNotNullOrEmpty]
        [Parameter(Position = 1, Mandatory = true)]
        public string Key { get; set; }
    }
}
