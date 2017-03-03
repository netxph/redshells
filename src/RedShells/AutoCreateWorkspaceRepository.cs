using System;
using RedShells.Core;
using RedShells.Core.Interfaces;

namespace RedShells
{

    public class AutoCreateWorkspaceRepository : IWorkspaceRepository
    {

        private readonly IWorkspaceRepository _repository;

        protected IWorkspaceRepository Repository { get { return _repository; } }

        public AutoCreateWorkspaceRepository(IWorkspaceRepository repository)
        {
            if(repository == null)
            {
                throw new ArgumentNullException("repository", "AutoCreateWorkspaceRepository:repository");
            }

            _repository = repository;
        }

        public Workspace Get(string name)
        {
            EnsureDatabaseExist();

            return Repository.Get(name);
        }

        public void Add(Workspace workspace)
        {
            EnsureDatabaseExist();
            Repository.Add(workspace);
        }

        public void Edit(Workspace workspace)
        {
            EnsureDatabaseExist();
            Repository.Edit(workspace);
        }

        protected virtual void EnsureDatabaseExist()
        {
        }
    }
}
