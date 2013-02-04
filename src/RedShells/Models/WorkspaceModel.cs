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
        public IShellContext Shell { get; set; }

        public void Add(string key, string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                path = Shell.GetCurrentPath();
            }

            if (string.IsNullOrEmpty(key)) throw new Exception("Key is required");

            Workspace workspace = new Workspace()
            {
                Key = key,
                Path = path
            };

            //try to get if existing
            var workspaceData = Data.Get(workspace.Key);

            if (workspaceData != null)
            {
                Data.Update(workspace);
            }
            else
            {
                Data.Create(workspace);
            }

            Shell.Write(string.Format("[{0}] '{1}' added.", workspace.Key, workspace.Path));
        }

        public void Add(string key)
        {
            Add(key, null);
        }
    }
}
