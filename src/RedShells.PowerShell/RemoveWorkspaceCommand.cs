using System.Management.Automation;

namespace RedShells.PowerShell
{

    [Cmdlet(VerbsCommon.Remove, "Workspace")]
    public class RemoveWorkspaceCommand: PSCmdlet
    {

        protected override void ProcessRecord()
        {
            WriteObject("Hello world!!!");
        }

    }

}
