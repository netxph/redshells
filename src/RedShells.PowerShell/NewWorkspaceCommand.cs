using System;
using System.Management.Automation;
using Microsoft.Data.Sqlite;
using RedShells;
using RedShells.Core.Interfaces;
using RedShells.Core;

namespace RedShells.PowerShell
{

    [Cmdlet(VerbsCommon.New, "Workspace")]
    public class NewWorkspaceCommand : PSCmdlet
    {

        private readonly IWorkspaceRepository _repository;

        protected IWorkspaceRepository Repository { get { return _repository; } }

        public NewWorkspaceCommand()
            : this(
                new WorkspaceRepository(
                    new SqliteConnection("Data Source=redshells.db;Version=3;")))
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

            var workspace = Repository.Get(Name);

            if(workspace != null)
            {
                Repository.Edit(workspace);
            }
            else
            {
                Repository.Add(new Workspace(Name, Directory));
            }

            WriteObject(string.Format("Workspace [{0}} created.", Name));
        }
    }
}
