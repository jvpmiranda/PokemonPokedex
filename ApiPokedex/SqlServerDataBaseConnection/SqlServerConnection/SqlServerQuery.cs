using System;
using System.Data;
using System.Data.SqlClient;

namespace SqlServerADOConnection.SQLConnection
{
    public abstract class SqlServerQuery
    {
        public SqlServerQuery(string connectionString)
        {
            ConnectionString = connectionString;
        }

        protected string ConnectionString;

        protected void Execute(string sqlCommand)
        {
            SqlCommand command = CreateConnection(sqlCommand);
            try
            {
                command.ExecuteNonQuery();
            }
            finally
            {
                CloseConnection(command.Connection);
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
                CloseConnection(command.Connection);
            }
        }

        private SqlCommand CreateConnection(string sqlCommand)
        {
            SqlCommand command = new SqlCommand(sqlCommand);
            command.Connection = CreateConnection();
            return command;
        }

        private SqlConnection CreateConnection()
        {
            var conn = new SqlConnection(ConnectionString);
            conn.Open();
            return conn;
        }

        private void CloseConnection(SqlConnection connection)
        {
            ArgumentNullException.ThrowIfNull(connection);
            connection.Close();
        }

    }
}
