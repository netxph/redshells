using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management.Automation;
using Microsoft.PowerShell.Commands;
using System.Collections;

namespace RedShells
{
    [Cmdlet(VerbsCommon.Add, "Workspace")]
    public class AddWorkspaceCommand : WorkspaceCommandBase
    {

        [ValidateNotNullOrEmpty]
        [Parameter(Position = 1, Mandatory = true)]
        public string Key { get; set; }

        protected override void ProcessRecord()
        {
            if (WorkspaceData.SaveWorkspace(Key, SessionState.Path.CurrentLocation.Path))
            {
                WriteObject(string.Format("{0} : [{1}] added.", Key, SessionState.Path.CurrentLocation.Path));
            }
            else
            {
                WriteObject(string.Format("Failed to add {0} : [{1}].", Key, SessionState.Path.CurrentLocation.Path));
            }
        }

    }
}
