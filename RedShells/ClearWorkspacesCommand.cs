using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management.Automation;

namespace RedShells
{
    [Cmdlet(VerbsCommon.Clear, "Workspaces", SupportsShouldProcess=true)]
    public class ClearWorkspacesCommand : WorkspaceCommandBase
    {

        protected override void ProcessRecord()
        {
            if (ShouldContinue("Are you sure you want to clear workspace?", "Clear Workspace"))
            {
                int affected = WorkspaceData.Clear();
                WriteObject(string.Format("Cleared {0} workspaces.", affected));
            }
        }

    }
}
