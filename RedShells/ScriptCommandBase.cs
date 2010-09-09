using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management.Automation;

namespace RedShells
{
    public class ScriptCommandBase : PSCmdlet
    {

        private ScriptData _scriptData = null;

        public ScriptData ScriptData
        {
            get
            {
                if (_scriptData == null)
                {
                    _scriptData = new ScriptData();
                }

                return _scriptData;
            }
        }

    }
}
