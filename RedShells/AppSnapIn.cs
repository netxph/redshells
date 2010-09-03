using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management.Automation;
using System.ComponentModel;

namespace RedShells
{

    [RunInstaller(true)]
    public class AppSnapIn : PSSnapIn
    {

        public const string NAME = "RedShells";
        public const string DESCRIPTION = "RedShells : PowerShell Tools for Developers.";
        public const string VENDOR = "Marc Vitalis";

        public override string Description
        {
            get { return DESCRIPTION; }
        }

        public override string Name
        {
            get { return NAME; }
        }

        public override string Vendor
        {
            get { return VENDOR; }
        }

    }
}
