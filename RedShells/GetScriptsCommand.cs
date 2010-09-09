using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management.Automation;

namespace RedShells
{
    [Cmdlet(VerbsCommon.Get, "Scripts")]
    public class GetScriptsCommand : ScriptCommandBase
    {

        protected override void ProcessRecord()
        {
            WriteObject(getView(ScriptData.Scripts.Values), true);
        }

        private List<ScriptView> getView(IEnumerable<Script> scripts)
        {
            List<ScriptView> view = new List<ScriptView>();

            foreach (Script script in scripts)
            {
                view.Add(new ScriptView() { Key = script.Key, HandleName = script.HandleName, Sequence = script.Sequence });
            }

            return view;
        }

    }
}
