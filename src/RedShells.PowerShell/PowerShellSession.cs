using System;
using System.Management.Automation;

namespace RedShells.PowerShell
{
    public class PowerShellSession : IConsoleSession
    {
        private const string LAST_DIRECTORY_KEY = "global:RS_LastDirectory";

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
            return Session.SessionState.Path.CurrentLocation.Path;
        }

        public string PopDirectory()
        {
            var lastDirectory = Session.SessionState.PSVariable.Get(LAST_DIRECTORY_KEY);

            if (lastDirectory != null)
            {
                return lastDirectory.Value.ToString();
            }

            return string.Empty;
        }

        public void PushDirectory(string directory)
        {
            Session.SessionState.PSVariable.Set(LAST_DIRECTORY_KEY, directory);
        }

        public virtual void RegisterCommand(PSCmdlet cmdlet)
        {
            Session = cmdlet;
        }
    }
}
