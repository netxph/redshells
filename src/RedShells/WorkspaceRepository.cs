using System;
using RedShells;
using RedShells.Core.Interfaces;
using RedShells.Core;

namespace RedShells
{
    public class WorkspaceRepository : IWorkspaceRepository
    {

        public WorkspaceRepository(Data.DataContext context)
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
