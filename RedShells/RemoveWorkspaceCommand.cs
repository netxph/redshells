using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management.Automation;

namespace RedShells
{
    [Cmdlet(VerbsCommon.Remove, "Workspace", SupportsShouldProcess=true)]
    public class RemoveWorkspaceCommand : WorkspaceCommandBase
    {

        [ValidateNotNullOrEmpty]
        [Parameter(Position = 1, Mandatory = true)]
        public string Key { get; set; }

        protected override void ProcessRecord()
        {
            if (ShouldContinue(string.Format("Are you sure you want to delete [{0}]?", Key), "Delete"))
            { 
                WorkspaceData.RemoveWorkspace(Key);
            }
        }

    }
}
