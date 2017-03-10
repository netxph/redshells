using System;
using System.Management.Automation;
using RedShells.Data;
using RedShells.Core.Interfaces;

namespace RedShells.PowerShell
{


    [Cmdlet(VerbsCommon.Set, "Workspace")]
    public class SetWorkspaceCommand: PSCmdlet
    {
        private readonly IWorkspaceRepository _repository;
        private readonly IConsoleSession _session;

        protected IWorkspaceRepository Repository { get { return _repository; } }
        protected IConsoleSession Session { get { return _session; } }

        public SetWorkspaceCommand()
            : this(
                new JsonWorkspaceRepository("workspace.json"),
                new PowerShellSession())
        {
            ((PowerShellSession)Session).RegisterCommand(this);
        }

        public SetWorkspaceCommand(IWorkspaceRepository repository, IConsoleSession session)
        {
            _repository = repository;
            _session = session;
        }

        
        [Parameter(Position = 0, Mandatory = true)]
        public string Name { get; set; }

        protected override void ProcessRecord()
        {
            var workspace = Repository.Get(Name);

            if(workspace != null)
            {
                Session.InvokeCommand($"Set-Location -Path '{workspace.Directory}'");
            }
        }
    }
}
