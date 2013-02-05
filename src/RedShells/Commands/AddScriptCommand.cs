using RedShells.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace RedShells.Commands
{

    [Cmdlet(VerbsCommon.Add, "Script")]
    public class AddScriptCommand : BaseCommand<ScriptModel>
    {

        [Parameter(Position = 0, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string Key { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string AppName { get; set; }

        [Parameter(Position = 2)]
        public string Sequence { get; set; }

        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            Model.Add(Key, AppName, Sequence);
        }

    }
}
