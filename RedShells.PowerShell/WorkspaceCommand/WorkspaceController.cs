using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RedShells.PowerShell.WorkspaceCommand
{
    public class WorkspaceController
    {
        public IWorkspaceView View { get; set; }

        public WorkspaceController(IWorkspaceView view)
        {
            View = view;
        }

        public void SaveWorkspace(string key, string path)
        {
            RedShellsDB db = new RedShellsDB();
            db.Workspaces.Add(new Workspace() { Key = key, Path = path });

            View.WriteObject("Added.");
        }
    }
}
