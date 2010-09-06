using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SQLite;
using System.Reflection;
using System.IO;

namespace RedShells
{
    public class WorkspaceData
    {

        const string CONNECTION_STRING = "Data Source=$(ASSEMBLY_PATH)\\redshells.sqlite3;Version=3;";
        const string SELECT_ALL_QUERY = "SELECT * FROM Workspace";
        const string INSERT_COMMAND = "INSERT INTO Workspace (Key, Path) VALUES (@key, @path)";
        const string UPDATE_COMMAND = "UPDATE Workspace SET Key=@key, Path=@path WHERE Key=@key";
        const string DELETE_ALL_COMMAND = "DELETE FROM Workspace";
        const string DELETE_COMMAND = "DELETE FROM Workspace WHERE Key=@key";

        private Dictionary<string, Workspace> _workspaces = null;

        public Dictionary<string, Workspace> Workspaces
        {
            get
            {
                if (_workspaces == null)
                {
                    _workspaces = GetWorkspaces();
                }

                return _workspaces;
            }
        }

        public WorkspaceData()
        {

        }

        public Workspace GetWorkspace(string key)
        {
            Workspace result = null;

            if (Workspaces.ContainsKey(key))
            {
                result = Workspaces[key];
            }

            return result;
        }

        public Dictionary<string, Workspace> GetWorkspaces()
        {
            string connection_string = CONNECTION_STRING.Replace("$(ASSEMBLY_PATH)", Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));

            Dictionary<string, Workspace> results = new Dictionary<string, Workspace>();

            using (SQLiteConnection connection = new SQLiteConnection(connection_string))
            {
                SQLiteCommand command = new SQLiteCommand(SELECT_ALL_QUERY, connection);
                connection.Open();

                SQLiteDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Workspace workspace = new Workspace();
                    workspace.ID = Convert.ToInt32(reader["ID"]);
                    workspace.Key = Convert.ToString(reader["Key"]);
                    workspace.Path = Convert.ToString(reader["Path"]);

                    results.Add(workspace.Key, workspace);
                }

                reader.Close();

                connection.Close();
                connection.Dispose();
            }


            return results;
        }

        public bool RemoveWorkspace(string key)
        {
            bool result = false;
            string connection_string = CONNECTION_STRING.Replace("$(ASSEMBLY_PATH)", Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));

            using (SQLiteConnection connection = new SQLiteConnection(connection_string))
            {
                SQLiteCommand command = new SQLiteCommand(DELETE_COMMAND, connection);
                command.Parameters.Add(new SQLiteParameter("@key", key));

                connection.Open();

                if (command.ExecuteNonQuery() > 0)
                {
                    result = true;
                    _workspaces = null;
                }

                connection.Close();
                connection.Dispose();
            }

            return result;
        }

        public bool SaveWorkspace(string key, string path)
        {
            bool result = false;
            string connection_string = CONNECTION_STRING.Replace("$(ASSEMBLY_PATH)", Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));

            using (SQLiteConnection connection = new SQLiteConnection(connection_string))
            {
                string commandString = null;

                if (Workspaces.ContainsKey(key))
                {
                    commandString = UPDATE_COMMAND;
                }
                else
                {
                    commandString = INSERT_COMMAND;
                }

                SQLiteCommand command = new SQLiteCommand(commandString, connection);
                command.Parameters.Add(new SQLiteParameter("@key", key));
                command.Parameters.Add(new SQLiteParameter("@path", path));

                connection.Open();

                if (command.ExecuteNonQuery() == 1)
                {
                    result = true;
                    _workspaces = null;
                }

                connection.Close();
                connection.Dispose();
            }


            return result;
        }

        public int Clear()
        {
            int affected = 0;
            string connection_string = CONNECTION_STRING.Replace("$(ASSEMBLY_PATH)", Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));

            using (SQLiteConnection connection = new SQLiteConnection(connection_string))
            {
                SQLiteCommand command = new SQLiteCommand(DELETE_ALL_COMMAND, connection);
                connection.Open();

                affected = command.ExecuteNonQuery();
                _workspaces = null;

                connection.Close();
                connection.Dispose();
            }

            return affected;
        }
    }
}
