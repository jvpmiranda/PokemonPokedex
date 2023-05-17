using SqlServerDataBaseConnection.Interface;
using System;
using System.Data;
using System.Data.SqlClient;

namespace SqlServerDataBaseConnection.SQLConnection
{
    internal class SqlServerConnection : ISqlConnection
    {
        public static ISqlConnection Instance = new SqlServerConnection();

        public IDbConnection CreateConnection(string dataSource, string userId, string password, string databaseName)
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

            //builder.DataSource = "<your_server.database.windows.net>";
            //builder.UserID = "<your_username>";
            //builder.Password = "<your_password>";
            //builder.InitialCatalog = "<your_database>";

            builder.DataSource = dataSource;
            builder.UserID = userId;
            builder.Password = password;
            builder.InitialCatalog = databaseName;

            var conn = new SqlConnection(builder.ConnectionString);
            conn.Open();
            return conn;
        }

        public void CloseConnection(IDbConnection connection)
        {
            if (connection == null)
                throw new ArgumentNullException("connection", "Parameter can't be null");
            
            connection.Close();
        }
    }
}
