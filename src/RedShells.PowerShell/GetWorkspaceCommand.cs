using System.Linq;
using System.Management.Automation;

using RedShells.Core.Interfaces;

namespace RedShells.PowerShell
{


    [Cmdlet(VerbsCommon.Get, "Workspace")]
    public class GetWorkspaceCommand: PSCmdlet
    {
        protected IWorkspaceRepository Repository { get; }
        protected IConsoleSession Session { get; }

        public GetWorkspaceCommand()
            : this(
                new Data.JsonWorkspaceRepository("workspace.json"),
                new PowerShellSession())
        {
            ((PowerShellSession)Session).RegisterCommand(this);
        }

        public GetWorkspaceCommand(IWorkspaceRepository repository, IConsoleSession session)
        {
            Repository = repository;
            Session = session;
        }

        [Parameter(Position = 0)]
        public string Name { get; set; }

        protected override void ProcessRecord()
        {
            if(string.IsNullOrEmpty(Name))
            {
                var workspaces = Repository.GetAll();

                Session.Write(workspaces.Select(w =>
                    new WorkspaceModel()
                    {
                        Name = w.Name,
                        Directory = w.Directory
                    }).ToList());
            }
            else
            {
                var workspace = Repository.Get(Name);

                if(workspace != null)
                {
                    Session.Write(new WorkspaceModel()
                    {
                        Name = workspace.Name,
                        Directory = workspace.Directory
                    });
                }
            }

        }
    }
}
