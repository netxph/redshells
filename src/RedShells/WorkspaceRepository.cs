using System;
using System.Data;
using RedShells;
using RedShells.Core.Interfaces;
using RedShells.Core;

namespace RedShells
{
    public class WorkspaceRepository : IWorkspaceRepository
    {

        public WorkspaceRepository(IDbConnection connection)
        {
        }

        public Workspace Get(string name)
        {
            throw new NotImplementedException();
        }

        public void Add(Workspace workspace)
        {
            throw new NotImplementedException();
        }

        public void Edit(Workspace workspace)
        {
            throw new NotImplementedException();
        }

    }
}
