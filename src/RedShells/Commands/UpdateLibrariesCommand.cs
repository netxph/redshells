using RedShells.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace RedShells.Commands
{

    [Cmdlet(VerbsData.Update, "Libraries")]
    public class UpdateLibrariesCommand : BaseCommand<DependencyModel>
    {
        [Parameter(Position = 0, Mandatory = true, ParameterSetName="Single")]
        public string Name { get; set; }

        [Parameter(Position = 1, ParameterSetName = "Single")]
        public string Source { get; set; }

        [Parameter(Position = 2, ParameterSetName = "Single")]
        public string Destination { get; set; }

        [Parameter(Position = 3, ParameterSetName = "Single")]
        public string OverrideFilter { get; set; }

        [Parameter(Position = 4, ParameterSetName = "Single")]
        public string Namespaces { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = "All")]
        public SwitchParameter All { get; set; }

        protected override void ProcessRecord()
        {
            base.ProcessRecord();
            if (ParameterSetName == "Single")
            {
                Model.Update(Name, Source, Destination, OverrideFilter, Namespaces);
            }
            else
            {
                Model.Update();
            }
        }

    }
}
