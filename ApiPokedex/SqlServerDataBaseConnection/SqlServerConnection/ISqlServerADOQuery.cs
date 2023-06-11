using System.Data;

namespace SqlServerADOConnection.SQLConnection;

public interface ISqlServerADOQuery
{
    void ExecuteNonQuery(string sqlCommand);
    void ExecuteNonQueryStoredProcedure<T>(string procName, T parameters);
    DataSet ExecuteQuery(string sqlCommand);
    DataSet ExecuteQueryStoredProcedure<T>(string procName, T parameters);
    void BeginTransaction();
    void CommitTransaction();
    void RollbackTransaction();
}