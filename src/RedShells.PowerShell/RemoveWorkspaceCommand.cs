using System.Management.Automation;

namespace RedShells.PowerShell
{

    [Cmdlet(VerbsCommon.Remove, "Workspace")]
    public class RemoveWorkspaceCommand: PSCmdlet
    {

        public IConsoleSession Session { get; }

        public RemoveWorkspaceCommand(IConsoleSession session)
        {
            Session = session;
        }

        [Parameter(Position = 0)]
        public string Name { get; set; }

        protected override void ProcessRecord()
        {
            Session.Write(new WorkspaceModel() { Name = Name });
        }

    }

}
