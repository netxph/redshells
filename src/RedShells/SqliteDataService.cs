using RedShells.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RedShells
{
    [Export(typeof(IDataService))]
    public class SqliteDataService : IDataService
    {
        const string CONNECTION_STRING = @"Data Source={0}\redshells.sqlite3;Version=3;";
        const string INSERT_COMMAND = @"INSERT INTO Workspace (WorkspaceKey, Path) VALUES (@WorkspaceKey, @Path)";
        const string GET_COMMAND = @"SELECT WorkspaceKey, Path FROM Workspace WHERE WorkspaceKey = @WorkspaceKey";
        const string UPDATE_COMMAND = @"UPDATE Workspace SET Path = @Path WHERE WorkspaceKey = @WorkspaceKey";
        const string GETALL_COMMAND = @"SELECT WorkspaceKey, Path FROM Workspace";
        const string DELETE_COMMAND = @"DELETE FROM Workspace WHERE WorkspaceKey = @WorkspaceKey";

        static SqliteDataService()
        {
            Environment.SetEnvironmentVariable("PreLoadSQLite_BaseDirectory", Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
        }

        protected string ConnectionString 
        { 
            get { return string.Format(CONNECTION_STRING, Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)); }
        }

        public void CreateWorkspace(Workspace workspace)
        {
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                SQLiteCommand command = new SQLiteCommand(INSERT_COMMAND, connection);
                command.Parameters.AddWithValue("@WorkspaceKey", workspace.Key);
                command.Parameters.AddWithValue("@Path", workspace.Path);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                finally
                {
                    command.Dispose();
                    connection.Close();
                }
            }

        }

        public Workspace GetWorkspace(string key)
        {
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {

                SQLiteCommand command = new SQLiteCommand(GET_COMMAND, connection);
                command.Parameters.AddWithValue("@WorkspaceKey", key);
                SQLiteDataReader reader = null;

                try
                {
                    connection.Open();
                    reader = command.ExecuteReader();

                    Workspace workspace = null;

                    while(reader.Read())
                    {
                        workspace = new Workspace();
                        workspace.Key = reader["WorkspaceKey"].ToString();
                        workspace.Path = reader["Path"].ToString();
                    }
                    
                    return workspace;
                }
                finally
                {
                    if(reader != null)
                    {
                        reader.Close();
                        reader.Dispose();
                    }
                       
                    command.Dispose();
                    connection.Close();
                }

            }
        }

        public void UpdateWorkspace(Workspace workspace)
        {
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                SQLiteCommand command = new SQLiteCommand(UPDATE_COMMAND, connection);
                command.Parameters.AddWithValue("@Path", workspace.Path);
                command.Parameters.AddWithValue("@WorkspaceKey", workspace.Key);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                finally
                {
                    command.Dispose();
                    connection.Close();
                }
            }
        }


        public List<Workspace> GetWorkspaces()
        {
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {

                SQLiteCommand command = new SQLiteCommand(GETALL_COMMAND, connection);
                SQLiteDataReader reader = null;

                try
                {
                    connection.Open();
                    reader = command.ExecuteReader();

                    List<Workspace> workspaces = new List<Workspace>();
                    while (reader.Read())
                    {
                        Workspace workspace = new Workspace() { Key = reader["WorkspaceKey"].ToString(), Path = reader["Path"].ToString() };
                        workspaces.Add(workspace);
                    }

                    return workspaces;
                }
                finally
                {
                    if (reader != null)
                    {
                        reader.Close();
                        reader.Dispose();
                    }

                    command.Dispose();
                    connection.Close();
                }
            }
        }

        public void RemoveWorkspace(string key)
        {
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                SQLiteCommand command = new SQLiteCommand(DELETE_COMMAND, connection);
                command.Parameters.AddWithValue("@WorkspaceKey", key);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                finally
                {
                    command.Dispose();
                    connection.Close();
                }
            }
        }
    }
}
