using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management.Automation;

namespace RedShells
{
    [Cmdlet(VerbsCommon.Remove, "Script", SupportsShouldProcess=true)]
    public class RemoveScriptCommand : ScriptCommandBase
    {

        [ValidateNotNullOrEmpty]
        [Parameter(Position = 1, Mandatory = true)]
        public string Key { get; set; }

        protected override void ProcessRecord()
        {
            if (ShouldContinue(string.Format("Are you sure you want to delete [{0}]?", Key), "Delete"))
            {
                ScriptData.RemoveScript(Key);
            }
        }    
    }
}
