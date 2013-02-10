using Microsoft.VisualBasic;
using RedShells.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RedShells
{
    [Export(typeof(IShellContext))]
    public class ShellContext : IShellContext
    {

        public ShellContext()
        {
            ContextStore = new Dictionary<string, string>();
        }

        protected PSCmdlet View { get; set; }

        public Dictionary<string, string> ContextStore { get; set; }

        public string GetCurrentPath()
        {
            return View.SessionState.Path.CurrentLocation.Path;
        }

        public void Initialize(PSCmdlet command)
        {
            View = command;
        }

        public void Write(string message)
        {
            View.WriteObject(message);
        }

        public void SetCurrentPath(string path)
        {
            View.InvokeCommand.InvokeScript(string.Format("Set-Location -Path '{0}'", path));
        }

        public void Write(IList list)
        {
            View.WriteObject(list, true);
        }

        public void RunScript(string applicationName, List<string> script)
        {
            Interaction.AppActivate(applicationName);

            foreach (var sequence in script)
            {
                Thread.Sleep(200);
                SendKeys.SendWait(sequence);
            }
        }

        public void SaveLocation(string source, string destination)
        {
            throw new NotImplementedException();
        }

        public void GetFiles(string files, string destination)
        {
            throw new NotImplementedException();
        }

        public DependencyPath RetrieveLocation()
        {
            throw new NotImplementedException();
        }
    }
}
