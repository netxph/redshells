using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedShells
{
    public class DependencyPath
    {

        public string Name { get; set; }
        public string Source { get; set; }
        public string Destination { get; set; }
        public string Filter { get; set; }
        public string Namespaces { get; set; }

    }
}
