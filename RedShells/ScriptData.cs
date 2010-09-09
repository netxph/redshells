using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Data.SQLite;

namespace RedShells
{
    public class ScriptData
    {

        const string CONNECTION_STRING = "Data Source=$(ASSEMBLY_PATH)\\redshells.sqlite3;Version=3;";
        const string SELECT_ALL_QUERY = "SELECT * FROM Script";
        const string INSERT_COMMAND = "INSERT INTO Script (Key, HandleName, Sequence) VALUES (@key, @handleName, @sequence)";
        const string UPDATE_COMMAND = "UPDATE Script SET Key=@key, HandleName=@handleName, Sequence=@sequence WHERE Key=@key";
        const string DELETE_ALL_COMMAND = "DELETE FROM Script";
        const string DELETE_COMMAND = "DELETE FROM Script WHERE Key=@key";

        private Dictionary<string, Script> _scripts = null;

        public Dictionary<string, Script> Scripts
        {
            get
            {
                if (_scripts == null)
                {
                    _scripts = GetScripts();
                }

                return _scripts;
            }
        }

        public Script GetScript(string key)
        {
            Script result = null;

            if (Scripts.ContainsKey(key))
            {
                result = Scripts[key];
            }

            return result;
        }

        public Dictionary<string, Script> GetScripts()
        {
            string connection_string = CONNECTION_STRING.Replace("$(ASSEMBLY_PATH)", Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));

            Dictionary<string, Script> results = new Dictionary<string, Script>();

            using (SQLiteConnection connection = new SQLiteConnection(connection_string))
            {
                SQLiteCommand command = new SQLiteCommand(SELECT_ALL_QUERY, connection);
                connection.Open();

                SQLiteDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Script script = new Script();
                    script.ID = Convert.ToInt32(reader["ID"]);
                    script.Key = Convert.ToString(reader["Key"]);
                    script.HandleName = Convert.ToString(reader["HandleName"]);
                    script.Sequence = Convert.ToString(reader["Sequence"]);

                    results.Add(script.Key, script);
                }

                reader.Close();

                connection.Close();
                connection.Dispose();
            }


            return results;
        }


        public bool SaveScript(string key, string handleName, string sequence)
        {
            bool result = false;
            string connection_string = CONNECTION_STRING.Replace("$(ASSEMBLY_PATH)", Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));

            using (SQLiteConnection connection = new SQLiteConnection(connection_string))
            {
                string commandString = null;

                if (Scripts.ContainsKey(key))
                {
                    commandString = UPDATE_COMMAND;
                }
                else
                {
                    commandString = INSERT_COMMAND;
                }

                SQLiteCommand command = new SQLiteCommand(commandString, connection);
                command.Parameters.Add(new SQLiteParameter("@key", key));
                command.Parameters.Add(new SQLiteParameter("@handleName", handleName));
                command.Parameters.Add(new SQLiteParameter("@sequence", sequence));

                connection.Open();

                if (command.ExecuteNonQuery() == 1)
                {
                    result = true;
                    _scripts = null;
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
                _scripts = null;

                connection.Close();
                connection.Dispose();
            }

            return affected;
        }

        public bool RemoveScript(string key)
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
                    _scripts = null;
                }

                connection.Close();
                connection.Dispose();
            }

            return result;
        }
    }
}
