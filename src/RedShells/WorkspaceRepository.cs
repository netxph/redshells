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

            Connection.Open();

            var command = Connection.CreateCommand();
            command.CommandText = "SELECT Name, Directory from Workspaces";
            var reader = command.ExecuteReader();

            while(reader.Read())
            {
                var workspace = new Workspace(reader["Name"].ToString(), reader["Directory"].ToString());

                return workspace;
            }

            Connection.Close();

            return null;

        }

        public void Add(Workspace workspace)
        {
            OnInitialize(Connection);

            Connection.Open();

            var command = Connection.CreateCommand();
            
            var nameParam = command.CreateParameter();
            nameParam.ParameterName = "Name";
            nameParam.Value = workspace.Name;

            var directoryParam = command.CreateParameter();
            directoryParam.ParameterName = "Directory";
            directoryParam.Value = workspace.Directory;

            command.CommandText = "INSERT INTO Workspaces (Name, Directory) VALUES (@Name, @Directory)";
            command.Parameters.Add(nameParam);
            command.Parameters.Add(directoryParam);

            command.ExecuteNonQuery();

            Connection.Close();

        }

        public void Edit(Workspace workspace)
        {
            OnInitialize(Connection);

            Connection.Open();

            var command = Connection.CreateCommand();
            
            var nameParam = command.CreateParameter();
            nameParam.ParameterName = "Name";
            nameParam.Value = workspace.Name;

            var directoryParam = command.CreateParameter();
            directoryParam.ParameterName = "Directory";
            directoryParam.Value = workspace.Directory;

            command.CommandText = "UPDATE Workspaces SET Directory = @Directory WHERE Name = @Name";
            command.Parameters.Add(nameParam);
            command.Parameters.Add(directoryParam);

            command.ExecuteNonQuery();
            Connection.Close();
        }

    }
}
