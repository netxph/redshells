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
            Update(name, sourcePath, destination, "*.dll,*.exe,*.pdb");
        }


        public void Update(string name, string sourcePath, string destination, string filter)
        {
            if (string.IsNullOrEmpty(name)) throw new Exception("Name is required");

            if (string.IsNullOrEmpty(sourcePath) || string.IsNullOrEmpty(destination) || string.IsNullOrEmpty(filter))
            {
                var path = Shell.RetrieveLocation(name);

                sourcePath = string.IsNullOrEmpty(sourcePath) && path != null ? path.Source : sourcePath;
                destination = string.IsNullOrEmpty(destination) && path != null ? path.Destination : destination;
                filter = string.IsNullOrEmpty(filter) && path != null ? path.Filter : filter;
            }

            if (string.IsNullOrEmpty(sourcePath)) throw new Exception("Memory file is missing, you should supply source path");
            if (string.IsNullOrEmpty(destination)) throw new Exception("Memory file is missing, you should supply destination");
            if (string.IsNullOrEmpty(filter)) filter = "*.dll,*.exe,*.pdb";

            if (!Path.IsPathRooted(sourcePath))
            {
                sourcePath = Path.GetFullPath(Path.Combine(Shell.GetCurrentPath(), sourcePath));
            }

            if (!Path.IsPathRooted(destination))
            {
                destination = Path.GetFullPath(Path.Combine(Shell.GetCurrentPath(), destination));
            }

            Shell.SaveLocation(new DependencyPath() { Name = name, Source = sourcePath, Destination = destination, Filter = filter });

            var filters = filter.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var filterString in filters)
            {
                Shell.GetFiles(string.Format(@"{0}\{1}", sourcePath, filterString.Trim()), destination);
            }

            Shell.Write("Update complete.");
        }
    }
}
