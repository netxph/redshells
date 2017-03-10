using System.Management.Automation;
using System;
using System.Reflection;

namespace RedShells.Test
{

    public static class TestExtensions
    {

        public static void InvokeCommand(this PSCmdlet cmdlet)
        {
            var cmdletType = typeof(PSCmdlet);
            var method = cmdletType.GetMethod("ProcessRecord", BindingFlags.Instance|BindingFlags.NonPublic);

            method.Invoke(cmdlet, new object[0]);
        }
    }
}
