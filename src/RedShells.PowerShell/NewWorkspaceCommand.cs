using System.Management.Automation;
using RedShells.Core;
using RedShells.Core.Interfaces;

namespace RedShells.PowerShell
{

	[Cmdlet(VerbsCommon.New, "Workspace")]
	public class NewWorkspaceCommand : PSCmdlet
	{
	    protected IWorkspaceRepository Repository { get; }
	    protected IConsoleSession Session { get; }

	    public NewWorkspaceCommand()
			: this(new Data.JsonWorkspaceRepository("workspace.json"),
			        new PowerShellSession())
		{
		    ((PowerShellSession)Session).RegisterCommand(this);
		}

		public NewWorkspaceCommand(IWorkspaceRepository repository, IConsoleSession session)
		{
			Repository = repository;
		    Session = session;
		}

		[Parameter(Position = 0, Mandatory = true)]
		public string Name { get; set; }

		[Parameter]
		public string Directory { get; set; }

		protected override void ProcessRecord()
		{
			if (string.IsNullOrEmpty(Directory))
			{
				Directory = Session.GetWorkingDirectory();
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

			Session.Write(workspace);
		}
	}
}

