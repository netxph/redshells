using RedShells.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RedShells
{
    [Export(typeof(IDataService))]
    public class LocalDBDataService : IDataService
    {
        const string CONNECTION_STRING = @"Server=(localdb)\v11.0;Integrated Security=true;AttachDbFileName={0}\redshells.mdf;";
        const string INSERT_COMMAND = @"INSERT INTO Workspace (WorkspaceKey, Path) VALUES (@WorkspaceKey, @Path)";
        const string GET_COMMAND = @"SELECT WorkspaceKey, Path FROM Workspace WHERE WorkspaceKey = @WorkspaceKey";
        const string UPDATE_COMMAND = @"UPDATE Workspace SET Path = @Path WHERE WorkspaceKey = @WorkspaceKey";

        protected string ConnectionString 
        { 
            get { return string.Format(CONNECTION_STRING, Assembly.GetExecutingAssembly().Location); }
        }

        public void Create(Workspace workspace)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand(INSERT_COMMAND, connection);
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


        public Workspace Get(string key)
        {
            using(SqlConnection connection = new SqlConnection(ConnectionString))
            {

                SqlCommand command = new SqlCommand(GET_COMMAND, connection);
                command.Parameters.AddWithValue("@WorkspaceKey", key);
                SqlDataReader reader = null;

                try
                {
                    connection.Open();
                    reader = command.ExecuteReader();

                    Workspace workspace = new Workspace();

                    while(reader.Read())
                    {
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

        public void Update(Workspace workspace)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand(UPDATE_COMMAND, connection);
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
    }
}
