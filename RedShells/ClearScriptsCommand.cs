using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management.Automation;

namespace RedShells
{
    [Cmdlet(VerbsCommon.Clear, "Scripts", SupportsShouldProcess=true)]
    public class ClearScriptsCommand : ScriptCommandBase
    {

        protected override void ProcessRecord()
        {
            if (ShouldContinue("Are you sure you want to clear script?", "Clear Script"))
            {
                int affected = ScriptData.Clear();
                WriteObject(string.Format("Cleared {0} scripts.", affected));
            }
        }

    }
}
