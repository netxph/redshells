using System;
using System.Collections.Generic;
using System.Linq;
using RedShells.Core.Interfaces;
using RedShells;
using System.IO;
using Jil;

namespace RedShells.Data
{
    public class JsonWorkspaceRepository : IWorkspaceRepository
    {

        private readonly string _dataFile;

        public string DataFile { get { return _dataFile; } }

        public JsonWorkspaceRepository(string dataFile)
        {
            var home = Environment.GetEnvironmentVariable("HOME");
            _dataFile = Path.Combine(Path.Combine(home, ".redshells"), dataFile);
        }

        protected virtual void EnsureDirectoryExist()
        {
            var home = Environment.GetEnvironmentVariable("HOME");
            var configPath = Path.Combine(home, ".redshells");

            if(!Directory.Exists(configPath))
            {
                Directory.CreateDirectory(configPath);
            }
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
