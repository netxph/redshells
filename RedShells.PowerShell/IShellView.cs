using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RedShells.PowerShell
{
    public interface IShellView
    {

        void WriteObject(object @object);

    }
}
