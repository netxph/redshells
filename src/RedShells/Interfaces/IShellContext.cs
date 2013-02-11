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

        string GetCurrentPath();

        void Initialize(PSCmdlet command);

        void Write(string message);

        void WriteVerbose(string message);

        void SetCurrentPath(string path);

        void Write(IList list);

        void RunScript(string applicationName, List<string> script);

        void SaveLocation(DependencyPath path);

        void GetFiles(string files, string destination);

        DependencyPath RetrieveLocation(string name);
    }
}
