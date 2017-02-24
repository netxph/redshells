using System;
using System.Management.Automation;
using RedShells;
using RedShells.Data;
using RedShells.Core.Interfaces;

namespace RedShells.PowerShell
{

    [Cmdlet(VerbsCommon.New, "Workspace")]
    public class NewWorkspaceCommand : PSCmdlet
    {

        private readonly IWorkspaceRepository _repository;

        protected IWorkspaceRepository Repository { get { return _repository; } }

        public NewWorkspaceCommand()
            : this(new WorkspaceRepository(new DataContext()))
        {
        }

        public NewWorkspaceCommand(IWorkspaceRepository repository)
        {
            if(repository == null)
            {
                throw new ArgumentNullException("NewWorkspaceCommand:repository");
            }

            _repository = repository;
        }

        [Parameter(Mandatory = true)]
        public string Name { get; set; }

        [Parameter]
        public string Directory { get; set; }

        protected override void ProcessRecord()
        {
            if(string.IsNullOrEmpty(Directory))
            {
                Directory = this.SessionState.Path.CurrentFileSystemLocation.Path;
            }

            WriteObject(string.Format("{0}:{1}", Name, Directory));
        }
    }
}

