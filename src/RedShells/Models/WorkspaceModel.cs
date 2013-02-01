using RedShells.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;

namespace RedShells.Models
{
    public class WorkspaceModel
    {

        [Import]
        public IDataService Data { get; set; }

        [Import]
        public IShellContext Context { get; set; }

        public void Add(string key, string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                path = Context.GetCurrentPath();
            }

            if (string.IsNullOrEmpty(key)) throw new Exception("Key is required");

            Workspace workspace = new Workspace()
            {
                Key = key,
                Path = path
            };

            Data.Create(workspace);
        }

        public void Add(string key)
        {
            Add(key, null);
        }
    }
}
