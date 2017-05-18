using System.Management.Automation;
using RedShells.Data;
using RedShells.Core.Interfaces;

namespace RedShells.PowerShell
{


    [Cmdlet(VerbsCommon.Set, "Workspace")]
    public class SetWorkspaceCommand: PSCmdlet
    {
        protected IWorkspaceRepository Repository { get; }
        protected IConsoleSession Session { get; }

        public SetWorkspaceCommand()
            : this(
                new JsonWorkspaceRepository("workspace.json"),
                new PowerShellSession())
        {
            ((PowerShellSession)Session).RegisterCommand(this);
        }

        public SetWorkspaceCommand(IWorkspaceRepository repository, IConsoleSession session)
        {
            Repository = repository;
            Session = session;
        }

        
        [Parameter(ParameterSetName = "DirectorySet", Position = 0, Mandatory = true)]
        public string Name { get; set; }

        [Parameter(ParameterSetName = "BackSet", Mandatory = true)]
        public SwitchParameter Back { get; set; }

        protected override void ProcessRecord()
        {
            if (Back.IsPresent)
            {
                var directory = Session.PopDirectory();

                if (!string.IsNullOrEmpty(directory))
                {
                    var lastDirectory = Session.GetWorkingDirectory();

                    Session.InvokeCommand($"Set-Location -Path '{directory}'");
                    Session.PushDirectory(lastDirectory);
                }
            }
            else
            {
                var workspace = Repository.Get(Name);

                if (workspace != null)
                {
                    Session.PushDirectory(Session.GetWorkingDirectory());
                    Session.InvokeCommand($"Set-Location -Path '{workspace.Directory}'");
                }
            }
        }
    }
}
