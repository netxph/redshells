using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management.Automation;

namespace RedShells
{
    [Cmdlet(VerbsCommon.Get, "Workspaces")]
    public class GetWorkspacesCommand : WorkspaceCommandBase
    {

        protected override void ProcessRecord()
        {
            WriteObject(getView(WorkspaceData.Workspaces.Values), true);
        }

        private List<WorkspaceView> getView(IEnumerable<Workspace> workspaces)
        {
            List<WorkspaceView> view = new List<WorkspaceView>();

            foreach (Workspace workspace in workspaces)
            {
                view.Add(new WorkspaceView() { Key = workspace.Key, Path = workspace.Path });
            }

            return view;
        }

    }
}
