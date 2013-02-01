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
    public class BaseCommand<TModel> : Cmdlet
        where TModel: new()
    {

        public BaseCommand()
        {
            
        }

        public TModel Model { get; set; }

        protected CompositionContainer ObjectManager { get; set; }

        public T CreateObject<T>()
            where T: new()
        {
            var obj = (T)Activator.CreateInstance(typeof(T));

            ObjectManager.ComposeParts(obj);

            return obj;
        }

        protected override void BeginProcessing()
        {
            base.BeginProcessing();

            var catalog = new TypeCatalog
            (
                typeof(ShellContext),
                typeof(LocalDBDataService)
            );

            ObjectManager = new CompositionContainer(catalog);

            Model = CreateObject<TModel>();
        }

    }
}
