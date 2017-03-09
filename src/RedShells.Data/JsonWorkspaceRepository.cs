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
            _dataFile = dataFile;
        }

        public Core.Workspace Get(string name)
        {
            if(File.Exists(DataFile))
            {
               var json = File.ReadAllText(DataFile); 
               var workspaces = JSON.Deserialize<IEnumerable<Core.Workspace>>(json);

                return workspaces.FirstOrDefault(w => w.Name == name);
            }

            return null;
        }

        public void Add(Core.Workspace workspace)
        {
            var workspaces = new List<Core.Workspace>();

            if(File.Exists(DataFile))
            {
            }
            else
            {
                workspaces.Add(workspace);
            }

            var json = JSON.Serialize(workspaces);

            File.WriteAllText(string.Format("$(ASSEMBLY_PATH)\\{0}", DataFile), json);
        }

        public void Edit(Core.Workspace workspace)
        {
            throw new NotImplementedException();
        }
        
    }
}
