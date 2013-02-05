using RedShells.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace RedShells.Commands
{

    [Cmdlet(VerbsCommon.Get, "Scripts")]
    public class GetScriptsCommand : BaseCommand<ScriptModel>
    {

        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            Model.GetAll();
        }

    }
}
