using System;
using RedShells;
using RedShells.Core.Interfaces;
using RedShells.Core;

namespace RedShells
{
    public class WorkspaceRepository : IWorkspaceRepository
    {
        private readonly Data.DataContext _context;

        protected Data.DataContext Context { get { return _context; } }

        public WorkspaceRepository(Data.DataContext context)
        {
            if(context == null)
            {
                throw new ArgumentNullException("context", "WorkspaceRepository:context");
            }

            _context = context;
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
