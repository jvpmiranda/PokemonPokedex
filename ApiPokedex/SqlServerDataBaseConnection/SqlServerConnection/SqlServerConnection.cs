using SqlServerDataBaseConnection.Interface;
using System;
using System.Data;
using System.Data.SqlClient;

namespace SqlServerDataBaseConnection.SQLConnection
{
    internal class SqlServerConnection : ISqlConnection
    {
        private readonly IDataBaseConnection _databaseConnection;

        public SqlServerConnection(IDataBaseConnection databaseConnection)
        {
            _databaseConnection = databaseConnection;
        }

        public IDbConnection CreateConnection()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

            builder.DataSource = _databaseConnection.DataSource;
            builder.UserID = _databaseConnection.UserId;
            builder.Password = _databaseConnection.Password;
            builder.InitialCatalog = _databaseConnection.DatabaseName;

            var conn = new SqlConnection(builder.ConnectionString);
            conn.Open();
            return conn;
        }

        public void CloseConnection(IDbConnection connection)
        {
            ArgumentNullException.ThrowIfNull(connection);
            connection.Close();
        }
    }
}
