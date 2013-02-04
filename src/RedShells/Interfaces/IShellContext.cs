using System;
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

        void SetCurrentPath(string path);
    }
}
