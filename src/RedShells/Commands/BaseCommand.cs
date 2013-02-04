using RedShells.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace RedShells.Commands
{
    public class BaseCommand<TModel> : PSCmdlet
        where TModel: new()
    {

        public BaseCommand()
        {
            
        }

        public TModel Model { get; set; }

        protected override void BeginProcessing()
        {
            var shell = ObjectManager.GetObject<IShellContext>();
            shell.Initialize(this);

            Model = ObjectManager.Initialize<TModel>();
        }

    }
}
