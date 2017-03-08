using System;
using System.Management.Automation;

namespace RedShells.Host
{
    class Program
    {
        static void Main(string[] args)
        {
           
            var ps = PowerShell.Create();
            ps.AddCommand("Get-ChildItem");
            ps.Invoke();
        }
    }
}
