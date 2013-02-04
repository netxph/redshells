using RedShells.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace RedShells
{
    [Export(typeof(IShellContext))]
    public class ShellContext : IShellContext
    {

        protected PSCmdlet View { get; set; }


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
    }
}
