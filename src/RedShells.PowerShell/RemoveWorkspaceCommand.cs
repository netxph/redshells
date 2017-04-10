using System.Collections.Generic;
using System.Management.Automation;
using RedShells.Core.Interfaces;
using RedShells.Data;

namespace RedShells.PowerShell
{

    [Cmdlet(VerbsCommon.Remove, "Workspace")]
    public class RemoveWorkspaceCommand: PSCmdlet
    {

        public IConsoleSession Session { get; }
        public IWorkspaceRepository Repository { get; }

        public RemoveWorkspaceCommand()
            : this(
                new JsonWorkspaceRepository("workspace.json"),
                new PowerShellSession())
        {
            ((PowerShellSession)Session).RegisterCommand(this);
        }

        public RemoveWorkspaceCommand(IWorkspaceRepository repository, IConsoleSession session)
        {
            Session = session;
            Repository = repository;
        }

        [Parameter(Position = 0)]
        public string Name { get; set; }

        protected override void ProcessRecord()
        {
            if (!string.IsNullOrEmpty(Name))
            {
                var workspace = Repository.Get(Name);

                Repository.Delete(Name);

                Session.Write(workspace);
            }
            else
            {
                var workspaces = Repository.GetAll();

                Repository.DeleteAll();

                Session.Write(workspaces);
            }
        }

    }

}
