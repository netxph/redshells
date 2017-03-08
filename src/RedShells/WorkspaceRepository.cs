using System;
using System.Data;
using RedShells;
using RedShells.Core.Interfaces;
using RedShells.Core;

namespace RedShells
{
    public class WorkspaceRepository : IWorkspaceRepository
    {

        private readonly IDbConnection _connection;

        protected IDbConnection Connection { get { return _connection; } }

        public WorkspaceRepository(IDbConnection connection)
        {
            if(connection == null)
            {
                throw new ArgumentNullException(nameof(connection), "WorkspaceRepository:connection");
            }

            _connection = connection;
        }

        protected virtual void OnInitialize(IDbConnection connection)
        {
            var bootstrap = new Data.SqliteBootstrap(connection);
            bootstrap.Start();
        }

        public Workspace Get(string name)
        {
            OnInitialize(Connection);

            throw new NotImplementedException();
        }

        public void Add(Workspace workspace)
        {
            OnInitialize(Connection);

            throw new NotImplementedException();
        }

        public void Edit(Workspace workspace)
        {
            throw new NotImplementedException();
        }

    }
}
