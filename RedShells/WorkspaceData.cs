using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SQLite;

namespace RedShells
{
    public class WorkspaceData
    {

        const string CONNECTION_STRING = "Data Source=redshells.sqlite3;Version=3;";
        const string SELECT_ALL_QUERY = "SELECT * FROM Workspace";

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
            Dictionary<string, Workspace> results = new Dictionary<string, Workspace>();

            using (SQLiteConnection connection = new SQLiteConnection(CONNECTION_STRING))
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

    }
}
