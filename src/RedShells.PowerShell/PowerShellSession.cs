using System;
using System.Management.Automation;

namespace RedShells.PowerShell
{
    public class PowerShellSession : IConsoleSession
    {

        protected PSCmdlet Session { get; set; }

        public void Write(object @object)
        {
            Session.WriteObject(@object);
        }

        public void InvokeCommand(string command)
        {
            Session.InvokeCommand.InvokeScript(command);
        }
        
        public string GetWorkingDirectory()
        {
            throw new NotImplementedException();
        }

        public virtual void RegisterCommand(PSCmdlet cmdlet)
        {
            Session = cmdlet;
        }
    }
}
