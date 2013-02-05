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
        const string INSERT_WORKSPACE_COMMAND = @"INSERT INTO Workspace (WorkspaceKey, Path) VALUES (@WorkspaceKey, @Path)";
        const string GET_WORKSPACE_COMMAND = @"SELECT WorkspaceKey, Path FROM Workspace WHERE WorkspaceKey = @WorkspaceKey";
        const string UPDATE_WORKSPACE_COMMAND = @"UPDATE Workspace SET Path = @Path WHERE WorkspaceKey = @WorkspaceKey";
        const string GETALL_WORKSPACE_COMMAND = @"SELECT WorkspaceKey, Path FROM Workspace";
        const string DELETE_WORKSPACE_COMMAND = @"DELETE FROM Workspace WHERE WorkspaceKey = @WorkspaceKey";
        const string INSERT_SCRIPT_COMMAND = @"INSERT INTO Script (ScriptKey, ApplicationName, Sequence) VALUES (@ScriptKey, @ApplicationName, @Sequence)";
        const string UPDATE_SCRIPT_COMMAND = @"UPDATE Script SET ApplicationName = @ApplicationName, Sequence = @Sequence WHERE ScriptKey = @ScriptKey";
        const string GET_SCRIPT_COMMAND = @"SELECT ScriptKey, ApplicationName, Sequence FROM Script WHERE ScriptKey = @ScriptKey";
        const string GETALL_SCRIPT_COMMAND = @"SELECT ScriptKey, ApplicationName, Sequence FROM Script";
        const string DELETE_SCRIPT_COMMAND = @"DELETE FROM Script WHERE ScriptKey = @ScriptKey";

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
                SQLiteCommand command = new SQLiteCommand(INSERT_WORKSPACE_COMMAND, connection);
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

                SQLiteCommand command = new SQLiteCommand(GET_WORKSPACE_COMMAND, connection);
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
                SQLiteCommand command = new SQLiteCommand(UPDATE_WORKSPACE_COMMAND, connection);
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

                SQLiteCommand command = new SQLiteCommand(GETALL_WORKSPACE_COMMAND, connection);
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
                SQLiteCommand command = new SQLiteCommand(DELETE_WORKSPACE_COMMAND, connection);
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

        public void CreateScript(Script script)
        {
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                SQLiteCommand command = new SQLiteCommand(INSERT_SCRIPT_COMMAND, connection);
                command.Parameters.AddWithValue("@ScriptKey", script.Key);
                command.Parameters.AddWithValue("@ApplicationName", script.ApplicationName);
                command.Parameters.AddWithValue("@Sequence", script.Sequence);

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

        public void UpdateScript(Script script)
        {
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                SQLiteCommand command = new SQLiteCommand(UPDATE_SCRIPT_COMMAND, connection);
                command.Parameters.AddWithValue("@ScriptKey", script.Key);
                command.Parameters.AddWithValue("@ApplicationName", script.ApplicationName);
                command.Parameters.AddWithValue("@Sequence", script.Sequence);

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


        public Script GetScript(string key)
        {
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {

                SQLiteCommand command = new SQLiteCommand(GET_SCRIPT_COMMAND, connection);
                command.Parameters.AddWithValue("@ScriptKey", key);
                SQLiteDataReader reader = null;

                try
                {
                    connection.Open();
                    reader = command.ExecuteReader();

                    Script script = null;

                    while (reader.Read())
                    {
                        script = new Script();
                        script.Key = reader["ScriptKey"].ToString();
                        script.ApplicationName = reader["ApplicationName"].ToString();
                        script.Sequence = reader["Sequence"].ToString();
                    }

                    return script;
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


        public List<Script> GetScripts()
        {
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {

                SQLiteCommand command = new SQLiteCommand(GETALL_SCRIPT_COMMAND, connection);
                SQLiteDataReader reader = null;

                try
                {
                    connection.Open();
                    reader = command.ExecuteReader();

                    List<Script> scripts = new List<Script>();
                    while (reader.Read())
                    {
                        Script script = new Script() { Key = reader["ScriptKey"].ToString(), ApplicationName = reader["ApplicationName"].ToString(), Sequence = reader["Sequence"].ToString() };
                        scripts.Add(script);
                    }

                    return scripts;
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


        public void RemoveScript(string key)
        {
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                SQLiteCommand command = new SQLiteCommand(DELETE_SCRIPT_COMMAND, connection);
                command.Parameters.AddWithValue("@ScriptKey", key);

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
