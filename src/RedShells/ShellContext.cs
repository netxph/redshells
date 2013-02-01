using RedShells.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedShells
{
    [Export(typeof(IShellContext))]
    public class ShellContext : IShellContext
    {
        public string GetCurrentPath()
        {
            throw new NotImplementedException();
        }
    }
}
