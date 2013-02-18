using RedShells.Listeners;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedShells
{
    public class ObjectManager
    {

        protected static CompositionContainer Container = null;

        static ObjectManager()
        {
            var catalog = new TypeCatalog
            (
                typeof(ShellContext),
                typeof(SqliteDataService),
                typeof(PersistenceStore),
                typeof(FileChangeListener)
            );

            Container = new CompositionContainer(catalog);
        }

        public static T GetObject<T>()
        {
            return Container.GetExportedValueOrDefault<T>();
        }

        public static T Initialize<T>()
            where T : new()
        {
            var obj = (T)Activator.CreateInstance(typeof(T));

            return Initialize(obj);
        }

        public static T Initialize<T>(T obj)
        {
            Container.ComposeParts(obj);

            return obj;
        }

    }
}
