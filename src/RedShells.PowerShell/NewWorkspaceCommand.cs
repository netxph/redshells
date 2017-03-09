using System;
using System.Management.Automation;
using RedShells.Core;
using RedShells.Core.Interfaces;
using RedShells;

namespace RedShells.PowerShell
{

	[Cmdlet(VerbsCommon.New, "Workspace")]
	public class NewWorkspaceCommand : PSCmdlet
	{

		private readonly IWorkspaceRepository _repository;

		protected IWorkspaceRepository Repository { get { return _repository; } }

		public NewWorkspaceCommand()
			: this(new Data.JsonWorkspaceRepository("workspace.json"))
		{
		}

		public NewWorkspaceCommand(IWorkspaceRepository repository)
		{
			if (repository == null)
			{
				throw new ArgumentNullException("repository", "NewWorkspaceCommand:repository");
			}

			_repository = repository;
		}

		[Parameter(Position = 0, Mandatory = true)]
		public string Name { get; set; }

		[Parameter]
		public string Directory { get; set; }

		protected override void ProcessRecord()
		{
			if (string.IsNullOrEmpty(Directory))
			{
				Directory = this.SessionState.Path.CurrentFileSystemLocation.Path;
			}

			var workspace = Repository.Get(Name);

			if (workspace != null)
			{
			    workspace.SetDirectory(Directory);
				Repository.Edit(workspace);
			}
			else
			{
			    workspace = new Workspace(Name, Directory);
				Repository.Add(workspace);
			}

			WriteObject(workspace);
		}
	}
}

