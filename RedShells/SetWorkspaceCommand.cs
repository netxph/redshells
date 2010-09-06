using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management.Automation;
using Microsoft.PowerShell.Commands;
using System.Collections.ObjectModel;
using System.Management.Automation.Runspaces;
using System.Reflection;
using System.IO;

namespace RedShells
{
    [Cmdlet(VerbsCommon.Set, "Workspace")]
    public class SetWorkspaceCommand : WorkspaceCommandBase
    {

        const string COMMAND = "Set-Location";
        const string PATH_PARAM = "Path";

        [Parameter(Position=1, Mandatory=true)]
        public string Name { get; set; }

        protected override void ProcessRecord()
        {
            Workspace workspace = WorkspaceData.GetWorkspace(Name);

            string command = string.Format("{0} -{1} {2}", COMMAND, PATH_PARAM, workspace.Path);
            WriteVerbose(string.Format("Changing Directory: {0}", Name));
            Collection<PSObject> results = this.InvokeCommand.InvokeScript(command);
        }

    }
}
