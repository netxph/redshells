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
            var workspaceData = Data.GetWorkspace(workspace.Key);

            if (workspaceData != null)
            {
                Data.UpdateWorkspace(workspace);
            }
            else
            {
                Data.CreateWorkspace(workspace);
            }

            Shell.Write(string.Format("[{0}] '{1}' added.", workspace.Key, workspace.Path));
        }

        public void Add(string key)
        {
            Add(key, null);
        }

        public void Set(string key)
        {
            if (string.IsNullOrEmpty(key)) throw new Exception("Key is required");

            var workspace = Data.GetWorkspace(key);

            if (workspace != null)
            {
                Shell.SetCurrentPath(workspace.Path);
            }
            else
            {
                throw new Exception("Workspace key does not exist");
            }
        }

        public void GetAll()
        {
            var workspaces = Data.GetWorkspaces();

            Shell.Write(workspaces);
        }

        public void Remove(string key)
        {
            if (string.IsNullOrEmpty(key)) throw new Exception("Key is required");

            Data.RemoveWorkspace(key);

            Shell.Write(string.Format("[{0}] removed.", key));
        }
    }
}
