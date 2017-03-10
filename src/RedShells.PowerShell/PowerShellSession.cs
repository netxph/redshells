using System;
using System.Management.Automation;

namespace RedShells.PowerShell
{
    public class PowerShellSession : IConsoleSession
    {
        public void Write(object @object)
        {
            throw new NotImplementedException();
        }

        public void InvokeCommand(string command)
        {
            throw new NotImplementedException();
        }

        public virtual void RegisterCommand(PSCmdlet cmdlet)
        {
            throw new NotImplementedException();
        }
    }
}
