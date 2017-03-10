using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System.Management.Automation;

using RedShells.Core.Interfaces;

namespace RedShells.PowerShell
{


    [Cmdlet(VerbsCommon.Get, "Workspace")]
    public class GetWorkspaceCommand: PSCmdlet
    {

        private readonly IWorkspaceRepository _repository;
        private readonly IConsoleSession _session;

        protected IWorkspaceRepository Repository { get { return _repository; } }
        protected IConsoleSession Session { get { return _session; } }

        public GetWorkspaceCommand()
            : this(
                new Data.JsonWorkspaceRepository("workspace.json"),
                new PowerShellSession())
        {
            ((PowerShellSession)Session).RegisterCommand(this);
        }

        public GetWorkspaceCommand(IWorkspaceRepository repository, IConsoleSession session)
        {
            _repository = repository;
            _session = session;
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
