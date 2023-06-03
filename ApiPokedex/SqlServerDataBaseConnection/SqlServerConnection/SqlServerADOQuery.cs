using System;
using System.Data;
using System.Data.SqlClient;

namespace SqlServerADOConnection.SQLConnection
{
    public class SqlServerADOQuery : ISqlServerADOQuery
    {
        private SqlConnection _sqlConnection;
        private SqlTransaction _sqlTransaction;
        private readonly string _connectionString;
        
        public SqlServerADOQuery(string connectionString)
        {
            _connectionString = connectionString;
        }
        
        #region ISqlServerQuery

        public void ExecuteNonQuery(string sqlCommand)
        {
            SqlCommand command = CreateConnection(sqlCommand);
            ExecuteNonQuery(command);
        }

        public DataSet ExecuteQuery(string sqlCommand)
        {
            SqlCommand command = CreateConnection(sqlCommand);
            return ExecuteReadDataSet(command);
        }

        public void BeginTransaction()
        {
            _sqlTransaction = _sqlConnection.BeginTransaction();
        }

        public void CommitTransaction()
        {
            _sqlTransaction.Commit();
            _sqlTransaction = null;
            CloseConnection();
        }

        public void RollbackTransaction()
        {
            _sqlTransaction.Rollback();
            _sqlTransaction = null;
            CloseConnection();
        }

        public void ExecuteNonQueryStoredProcedure<T>(string procName, T parameters)
        {
            SqlCommand command = CreateSqlCommandStoredProcedure(procName, parameters);
            ExecuteNonQuery(command);
        }

        public DataSet ExecuteQueryStoredProcedure<T>(string procName, T parameters)
        {
            SqlCommand command = CreateSqlCommandStoredProcedure(procName, parameters);
            return ExecuteReadDataSet(command);
        }
        #endregion ISqlServerQuery

        private void ExecuteNonQuery(SqlCommand command)
        {
            try
            {
                command.ExecuteNonQuery();
            }
            finally
            {
                CloseConnection();
            }
        }

        private SqlCommand CreateSqlCommandStoredProcedure<T>(string procName, T parameters)
        {
            SqlCommand command = CreateConnection(procName);
            command.CommandType = CommandType.StoredProcedure;
            foreach (var prop in typeof(T).GetProperties())
            {
                var value = prop.GetValue(parameters).ToString();
                command.Parameters.Add(new SqlParameter(prop.Name, value));
            }

            return command;
        }

        private DataSet ExecuteReadDataSet(SqlCommand command)
        {
            try
            {
                IDataAdapter dataAdap = new SqlDataAdapter(command);
                DataSet data = new DataSet();
                dataAdap.Fill(data);
                return data;
            }
            finally
            {
                CloseConnection();
            }
        }

        private SqlCommand CreateConnection(string sqlCommand)
        {
            ArgumentNullException.ThrowIfNull(sqlCommand);
            CreateConnection();
            SqlCommand command = new SqlCommand(sqlCommand, _sqlConnection, _sqlTransaction);
            command.Connection.Open();
            return command;
        }

        private void CreateConnection()
        {
            if (_sqlTransaction == null)
                _sqlConnection = new SqlConnection(_connectionString);
        }

        private void CloseConnection()
        {
            ArgumentNullException.ThrowIfNull(_sqlConnection);

            if (_sqlTransaction == null)
            {
                _sqlConnection.Close();
                _sqlConnection = null;
            }
        }
    }
}
