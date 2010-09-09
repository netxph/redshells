using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management.Automation;

namespace RedShells
{
    [Cmdlet(VerbsCommon.Add, "Script")]
    public class AddScriptCommand : ScriptCommandBase
    {
        [ValidateNotNullOrEmpty]
        [Parameter(Position = 1, Mandatory = true)]
        public string Key { get; set; }

        [ValidateNotNullOrEmpty]
        [Parameter(Position=2, Mandatory=true)]
        public string HandleName { get; set; }

        [ValidateNotNullOrEmpty]
        [Parameter(Position=3, Mandatory=true)]
        public string Sequence { get; set; }

        protected override void ProcessRecord()
        {
            if (ScriptData.SaveScript(Key, HandleName, Sequence))
            {
                WriteObject(string.Format("[{0}] added.", Key));
            }
            else
            {
                WriteObject(string.Format("Failed to add [{0}].", Key));
            }
        }


    }
}
