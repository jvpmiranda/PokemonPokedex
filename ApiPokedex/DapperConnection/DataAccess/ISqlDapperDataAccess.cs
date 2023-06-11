using Dapper;

namespace DapperConnection.DataAccess;

public interface ISqlDapperDataAccess
{
    Task ExecuteNonQuery<T>(string command, T parameters);
    Task ExecuteNonQueryStoredProcedure<T>(string command, T parameters);
    Task<IEnumerable<T>> ExecuteQuery<T, U>(string command, U parameters);
    Task<IEnumerable<T>> ExecuteQueryStoredProcedure<T, U>(string command, U parameters);
    Task<IEnumerable<V>> ExecuteQueryStoredProcedure<T, U, V, W>(string command, Func<T, U, V> map, W parameters, string splitOn = "");
    Task<IEnumerable<W>> ExecuteQueryStoredProcedure<T, U, V, W, X>(string command, Func<T, U, V, W> map, X parameters, string splitOn = "");
   Task<SqlMapper.GridReader> ExecuteQueryStoredProcedureMultiple<T>(string command, T parameters);
}