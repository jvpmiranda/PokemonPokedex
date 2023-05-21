using System.Data;

namespace SqlServerADOConnection.SQLConnection
{
    public interface ISqlServerQuery
    {
        void ExecuteNonQuery(string sqlCommand);
        DataSet ExecuteQuery(string sqlCommand);
        void BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();
        void ExecuteStoredProcedure<T>(string procName, T parameters);
        DataSet ReadStoredProcedure<T>(string procName, T parameters);
    }
}