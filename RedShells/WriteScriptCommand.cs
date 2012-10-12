using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management.Automation;
using System.Runtime.InteropServices;
using Microsoft.VisualBasic;
using System.Windows.Forms;
using System.Threading;

namespace RedShells
{
    [Cmdlet(VerbsCommunications.Write, "Script")]
    public class WriteScriptCommand : ScriptCommandBase
    {

        [ValidateNotNullOrEmpty]
        [Parameter(Position=1, Mandatory=true)]
        public string Key { get; set; }

        protected override void ProcessRecord()
        {
            Script script = ScriptData.GetScript(Key);

            if (script != null)
            {
                //Interaction.AppActivate(script.HandleName);

                string[] sequences = script.Sequence.Split('|');

                foreach (string sequence in sequences)
                {
                    Thread.Sleep(200);
                    SendKeys.SendWait(sequence);
                }
            }
            else
            {
                WriteError(new ErrorRecord(new NullReferenceException("Script does not exist."), "0201", ErrorCategory.InvalidArgument, script));
            }

        }

    }
}
