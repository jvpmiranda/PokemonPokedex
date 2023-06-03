using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace DapperConnection.DataAccess
{
    public class SqlDapperDataAccess : ISqlDapperDataAccess
    {
        private readonly string _connectionString;

        public SqlDapperDataAccess(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<T>> ExecuteQuery<T, U>(string command, U parameters)
        {
            using IDbConnection conn = new SqlConnection(_connectionString);
            return await conn.QueryAsync<T>(command, parameters);
        }
        public async Task<IEnumerable<T>> ExecuteQueryStoredProcedure<T, U>(string command, U parameters)
        {
            using IDbConnection conn = new SqlConnection(_connectionString);
            return await conn.QueryAsync<T>(command, parameters, commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<V>> ExecuteQueryStoredProcedure<T, U, V, W>(string command, Func<T, U, V> map, W parameters)
        {
            using IDbConnection conn = new SqlConnection(_connectionString);
            return await conn.QueryAsync(command, map, parameters, commandType: CommandType.StoredProcedure);
        }

        public async Task<SqlMapper.GridReader> ExecuteQueryStoredProcedureMultiple<T>(string command, T parameters)
        {
            IDbConnection conn = new SqlConnection(_connectionString);
            return await conn.QueryMultipleAsync(command, parameters, commandType: CommandType.StoredProcedure);
        }

        public async Task ExecuteNonQuery<T>(string command, T parameters)
        {
            using IDbConnection conn = new SqlConnection(_connectionString);
            await conn.ExecuteAsync(command, parameters);
        }

        public async Task ExecuteNonQueryStoredProcedure<T>(string command, T parameters)
        {
            using IDbConnection conn = new SqlConnection(_connectionString);
            await conn.ExecuteAsync(command, parameters, commandType: CommandType.StoredProcedure);
        }
    }
}
