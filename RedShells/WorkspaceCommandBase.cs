using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management.Automation;

namespace RedShells
{
    public abstract class WorkspaceCommandBase : PSCmdlet
    {

        private static WorkspaceData _workspaceData = null;

        public static WorkspaceData WorkspaceData
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

    }
}
