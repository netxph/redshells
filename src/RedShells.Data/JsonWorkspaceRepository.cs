using System;
using System.Collections.Generic;
using System.Linq;
using RedShells.Core.Interfaces;
using System.IO;
using Jil;

namespace RedShells.Data
{
    public class JsonWorkspaceRepository : IWorkspaceRepository
    {
        public string DataFile { get; }

        public JsonWorkspaceRepository(string dataFile)
        {
            var home = Environment.GetEnvironmentVariable("HOME");
            if (string.IsNullOrEmpty(home))
            {
                home = Environment.GetEnvironmentVariable("LOCALAPPDATA");
            }

            DataFile = Path.Combine(Path.Combine(home, ".redshells"), dataFile);
        }

        protected virtual void EnsureDirectoryExist()
        {
            var home = Environment.GetEnvironmentVariable("HOME");

            if (string.IsNullOrEmpty(home))
            {
                home = Environment.GetEnvironmentVariable("LOCALAPPDATA");
            }

            var configPath = Path.Combine(home, ".redshells");

            if(!Directory.Exists(configPath))
            {
                Directory.CreateDirectory(configPath);
            }
        }

        public Core.Workspaces GetAll()
        {
            var workspaces = new Core.Workspaces();

            if(File.Exists(DataFile))
            {
                var json = File.ReadAllText(DataFile); 
                var data = JSON.Deserialize<IEnumerable<Workspace>>(json);

                foreach(var item in data)
                {
                    workspaces.Add(item.Name, item.Directory);
                }
            }

            return workspaces;
        }

        public Core.Workspace Get(string name)
        {
            if(File.Exists(DataFile))
            {
                var json = File.ReadAllText(DataFile); 
                var workspaces = JSON.Deserialize<IEnumerable<Workspace>>(json);

                var workspace = workspaces.FirstOrDefault(w => w.Name == name);

                if(workspace != null)
                {
                    return new Core.Workspace(workspace.Name, workspace.Directory);
                }
                
            }

            return null;
        }

        public void Add(Core.Workspace workspace)
        {
            EnsureDirectoryExist();

            var workspaces = new List<Workspace>();

            if(File.Exists(DataFile))
            {
                var json = File.ReadAllText(DataFile); 
                workspaces.AddRange(JSON.Deserialize<IEnumerable<Workspace>>(json));
            }

            workspaces.Add(new Workspace() { Name = workspace.Name, Directory = workspace.Directory } );

            File.WriteAllText(DataFile, JSON.Serialize(workspaces));
        }

        public void Edit(Core.Workspace workspace)
        {
            EnsureDirectoryExist();

            var workspaces = new List<Workspace>();

            if(File.Exists(DataFile))
            {
                var json = File.ReadAllText(DataFile); 
                workspaces.AddRange(JSON.Deserialize<IEnumerable<Workspace>>(json));
            }

            var data = workspaces.FirstOrDefault(w => w.Name == workspace.Name);

            if(data != null)
            {
                data.Directory = workspace.Directory;

                File.WriteAllText(DataFile, JSON.Serialize(workspaces));
            }
        }
        
    }
}
