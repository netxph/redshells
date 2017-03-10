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

        protected IWorkspaceRepository Repository { get { return _repository; } }

        public SetWorkspaceCommand()
            : this(
                new JsonWorkspaceRepository("workspace.json"),
                new PowerShellSession())
        {
        }

        public SetWorkspaceCommand(IWorkspaceRepository repository, IConsoleSession session)
        {
            _repository = repository;
        }

        
        [Parameter(Position = 0, Mandatory = true)]
        public string Name { get; set; }

        protected override void ProcessRecord()
        {
            var workspace = Repository.Get(Name);

            if(workspace != null)
            {
                this.InvokeCommand.InvokeScript($"Set-Location -Path '{workspace.Directory}'");
            }
        }
    }
}
