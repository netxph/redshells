using System;
using RedShells.Core.Interfaces;
using RedShells;
using System.IO;

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
            }

            return null;
        }

        public void Add(Core.Workspace workspace)
        {
            throw new NotImplementedException();
        }

        public void Edit(Core.Workspace workspace)
        {
            throw new NotImplementedException();
        }
        
    }
}
