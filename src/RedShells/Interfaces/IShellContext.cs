using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;

namespace RedShells.Interfaces
{
    public interface IShellContext
    {
        //TODO: Move this to its own class
        Dictionary<string, string> ContextStore { get; set; }

        string GetCurrentPath();

        void Initialize(PSCmdlet command);

        void Write(string message);

        void SetCurrentPath(string path);

        void Write(IList list);

        void RunScript(string applicationName, List<string> script);
    }
}
