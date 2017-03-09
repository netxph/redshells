using System;
using System.Diagnostics;
using System.Management.Automation;

using RedShells.Core.Interfaces;

namespace RedShells.PowerShell
{


    [Cmdlet(VerbsCommon.Get, "Workspace")]
    public class GetWorkspaceCommand: PSCmdlet
    {

        private readonly IWorkspaceRepository _repository;

        protected IWorkspaceRepository Repository { get { return _repository; } }

        public GetWorkspaceCommand()
            : this(new Data.JsonWorkspaceRepository("workspace.json"))
        {
        }

        public GetWorkspaceCommand(IWorkspaceRepository repository)
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
                WriteObject(workspace);
            }

        }
    }
}
