using System;
using System.Data;

namespace RedShells.Data
{

    public class SqliteBootstrap
    {

        private IDbConnection _connection;

        protected IDbConnection Connection { get { return _connection; } }

        public SqliteBootstrap(IDbConnection connection)
        {

            if(connection == null)
            {
                throw new ArgumentNullException("connection", "SqliteBootstrap:connection");
            }

            _connection = connection;
        }

        public void Start()
        {
            Connection.Open();

            try
            {
                var command = Connection.CreateCommand();

                command.CommandText = "CREATE TABLE IF NOT EXISTS Workspaces(Name TEXT, Directory TEXT, PRIMARY KEY(Name ASC))";
                command.ExecuteNonQuery();
            }
            finally
            {
                Connection.Close();
            }
        }
    }
}
