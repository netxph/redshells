using System;
using System.Management.Automation;

namespace RedShells.PowerShell
{


    [Cmdlet(VerbsCommon.Set, "Workspace")]
    public class SetWorkspaceCommand: PSCmdlet
    {

        protected override void ProcessRecord()
        {
            WriteObject("Hello world!!!");
        }
    }
}
