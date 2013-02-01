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

        public void Create(Workspace workspace)
        {
            var location = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            using (SqlConnection connection = new SqlConnection(string.Format(CONNECTION_STRING, location)))
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
    }
}
