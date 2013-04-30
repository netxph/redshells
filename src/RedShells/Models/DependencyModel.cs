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

        public void Update(string name)
        {
            Update(name, null, null);
        }

        public void Update(string name, string sourcePath, string destination)
        {
            Update(name, sourcePath, destination, "<ns>.dll,<ns>.exe,<ns>.pdb", "*");
        }

        public void Update(string name, string sourcePath, string destination, string filter)
        {
            Update(name, sourcePath, destination, filter, "*");
        }

        public void Update(string name, string sourcePath, string destination, string filter, string namespaces)
        {
            if (string.IsNullOrEmpty(name)) throw new Exception("Name is required");

            if (string.IsNullOrEmpty(sourcePath) || string.IsNullOrEmpty(destination) || string.IsNullOrEmpty(filter) || string.IsNullOrEmpty(namespaces))
            {
                var path = Shell.RetrieveLocation(name);

                sourcePath = string.IsNullOrEmpty(sourcePath) && path != null ? path.Source : sourcePath;
                destination = string.IsNullOrEmpty(destination) && path != null ? path.Destination : destination;
                filter = string.IsNullOrEmpty(filter) && path != null ? path.Filter : filter;
                namespaces = string.IsNullOrEmpty(namespaces) && path != null ? path.Namespaces : namespaces;
            }

            if (string.IsNullOrEmpty(sourcePath)) throw new Exception("Memory file is missing, you should supply source path");
            if (string.IsNullOrEmpty(destination)) throw new Exception("Memory file is missing, you should supply destination");
            if (string.IsNullOrEmpty(filter)) filter = "<ns>.dll,<ns>.exe,<ns>.pdb";
            if (string.IsNullOrEmpty(namespaces)) namespaces = "*";

            if (!Path.IsPathRooted(sourcePath))
            {
                sourcePath = Path.GetFullPath(Path.Combine(Shell.GetCurrentPath(), sourcePath));
            }

            if (!Path.IsPathRooted(destination))
            {
                destination = Path.GetFullPath(Path.Combine(Shell.GetCurrentPath(), destination));
            }

            Shell.SaveLocation(new DependencyPath() { Name = name, Source = sourcePath, Destination = destination, Filter = filter });

            var filters = assembleFilter(filter, namespaces);

            foreach (var filterString in filters)
            {
                Shell.GetFiles(string.Format(@"{0}\{1}", sourcePath, filterString.Trim()), destination);
            }

            Shell.Write("Update complete.");
        }

        private List<string> assembleFilter(string filter, string namespaces)
        {
            var filters = new List<string>();
            var ns = namespaces.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var token in ns)
            {
                var tokenized_filter = filter.Replace("<ns>", token);
                filters.AddRange(tokenized_filter.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries));
            }

            return filters;
        }

        public void Update()
        {
            var paths = Shell.RetrieveAll();

            foreach (var path in paths)
            {
                Update(path.Name, path.Source, path.Destination, path.Filter, path.Namespaces);
            }
        }
    }
}
