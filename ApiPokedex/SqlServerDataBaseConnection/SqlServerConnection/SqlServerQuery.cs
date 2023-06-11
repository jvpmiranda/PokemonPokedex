using AppSettings;
using SqlServerDataBaseConnection.Interface;
using System.Data;
using System.Data.SqlClient;

namespace SqlServerDataBaseConnection.SQLConnection
{
    public abstract class SqlServerQuery
    {
        protected ISqlConnection SqlConnection = SqlServerConnection.Instance;

        protected void Execute(string sqlCommand)
        {
            SqlCommand command = CreateConnection(sqlCommand);
            try
            {
                command.ExecuteNonQuery();
            }
            finally
            {
                SqlConnection.CloseConnection(command.Connection);
            }
        }

        protected DataSet ExecuteQuery(string sqlCommand)
        {
            SqlCommand command = CreateConnection(sqlCommand);
            try
            {
                IDataAdapter dataAdap = new SqlDataAdapter(command);
                DataSet data = new DataSet();
                dataAdap.Fill(data);
                return data;
            }
            finally
            {
                SqlConnection.CloseConnection(command.Connection);
            }
        }

        private SqlCommand CreateConnection(string sqlCommand)
        {
            SqlCommand command = new SqlCommand(sqlCommand);
            command.Connection = (SqlConnection)SqlConnection.CreateConnection(AppSettingsReader.DataBaseDataSource, AppSettingsReader.DataBaseUsername, AppSettingsReader.DataBasePassword, AppSettingsReader.DataBaseName);
            return command;
        }
    }
}
