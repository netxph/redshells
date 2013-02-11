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

        [Parameter(Position = 0, Mandatory = true)]
        public string Name { get; set; }

        [Parameter(Position = 1)]
        public string Source { get; set; }

        [Parameter(Position = 2)]
        public string Destination { get; set; }

        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            Model.Update(Name, Source, Destination);
        }

    }
}
