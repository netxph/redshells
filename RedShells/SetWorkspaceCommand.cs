using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management.Automation;
using Microsoft.PowerShell.Commands;
using System.Collections.ObjectModel;
using System.Management.Automation.Runspaces;

namespace RedShells
{
    [Cmdlet(VerbsCommon.Set, "Workspace", SupportsShouldProcess=true)]
    public class SetWorkspaceCommand : PSCmdlet
    {

        const string COMMAND = "Set-Location";
        const string PATH_PARAM = "Path";

        private WorkspaceData _workspaceData = null;

        [Parameter(Position=1, Mandatory=true)]
        public string Name { get; set; }

        public WorkspaceData WorkspaceData
        {
            get
            {
                if (_workspaceData == null)
                {
                    _workspaceData = new WorkspaceData();
                }

                return _workspaceData;
            }
        }

        protected override void ProcessRecord()
        {
            Workspace workspace = WorkspaceData.GetWorkspace(Name);

            string command = string.Format("{0} -{1} {2}", COMMAND, PATH_PARAM, workspace.Path);
            WriteVerbose(string.Format("Changing Directory: {0}", Name));
            Collection<PSObject> results = this.InvokeCommand.InvokeScript(command);
        }

    }
}
