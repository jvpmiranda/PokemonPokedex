namespace DapperConnection.DataAccess
{
    public interface ISqlDapperDataAccess
    {
        Task ExecuteNonQuery<T>(string command, T parameters);
        Task ExecuteNonQueryStoredProcedure<T>(string command, T parameters);
        Task<IEnumerable<T>> ExecuteQuery<T, U>(string command, U parameters);
        Task<IEnumerable<T>> ExecuteQueryStoredProcedure<T, U>(string command, U parameters);
    }
}