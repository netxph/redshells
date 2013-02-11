using RedShells.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedShells.Models
{
    public class DependencyModel
    {

        [Import]
        public IShellContext Shell { get; set; }

        public void Update(string name, string sourcePath, string destination)
        {
            if (string.IsNullOrEmpty(name)) throw new Exception("Name is required");

            if (string.IsNullOrEmpty(sourcePath) || string.IsNullOrEmpty(destination))
            {
                var path = Shell.RetrieveLocation(name);

                sourcePath = string.IsNullOrEmpty(sourcePath) && path != null ? path.Source : sourcePath;
                destination = string.IsNullOrEmpty(destination) && path != null ? path.Destination : destination;
            }

            if (string.IsNullOrEmpty(sourcePath)) throw new Exception("Memory file is missing, you should supply source path");
            if (string.IsNullOrEmpty(destination)) throw new Exception("Memory file is missing, you should supply destination");

            if (!Path.IsPathRooted(sourcePath))
            {
                sourcePath = Path.GetFullPath(Path.Combine(Shell.GetCurrentPath(), sourcePath));
            }

            if (!Path.IsPathRooted(destination))
            {
                destination = Path.GetFullPath(Path.Combine(Shell.GetCurrentPath(), destination));
            }

            Shell.SaveLocation(new DependencyPath() { Name = name, Source = sourcePath, Destination = destination });

            Shell.GetFiles(string.Format(@"{0}\*.dll", sourcePath), destination);
            Shell.GetFiles(string.Format(@"{0}\*.exe", sourcePath), destination);
            Shell.GetFiles(string.Format(@"{0}\*.pdb", sourcePath), destination);

            Shell.Write("Update complete.");
        }
        
    }
}
