﻿using RedShells.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace RedShells.Commands
{

    [Cmdlet(VerbsCommon.Add, "Workspace")]
    public class AddWorkspaceCommand : BaseCommand<WorkspaceModel>
    {
        [Parameter(Position=0, Mandatory=true)]
        [ValidateNotNullOrEmpty]
        public string Key { get; set; }

        [Parameter(Position=1)]
        public string Path { get; set; }

        protected override void ProcessRecord()
        {
            base.ProcessRecord();
            Model.Add(Key, Path);
        }

    }
}
