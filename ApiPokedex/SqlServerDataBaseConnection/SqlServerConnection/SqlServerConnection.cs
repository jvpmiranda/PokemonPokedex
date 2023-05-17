using SqlServerDataBaseConnection.Interface;
using System;
using System.Data;
using System.Data.SqlClient;

namespace SqlServerDataBaseConnection.SQLConnection
{
    public class SqlServerConnection : ISqlConnection
    {
        private readonly string _connectionString;

        public SqlServerConnection(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IDbConnection CreateConnection()
        {
            var conn = new SqlConnection(_connectionString);
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
