using RedShells.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedShells.Models
{
    public class DependencyModel
    {

        public IShellContext Shell { get; set; }

        public void Update(string sourcePath, string destination)
        {
            if (string.IsNullOrEmpty(sourcePath) || string.IsNullOrEmpty(destination))
            {
                var path = Shell.RetrieveLocation();

                sourcePath = string.IsNullOrEmpty(sourcePath) ? path.Source : sourcePath;
                destination = string.IsNullOrEmpty(destination) ? path.Destination : destination;
            }

            Shell.SaveLocation(sourcePath, destination);

            Shell.GetFiles(string.Format(@"{0}\*.dll", sourcePath), destination);
            Shell.GetFiles(string.Format(@"{0}\*.exe", sourcePath), destination);
            Shell.GetFiles(string.Format(@"{0}\*.pdb", sourcePath), destination);
        }
        
    }
}
