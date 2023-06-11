using System.Data;

namespace SqlServerDataBaseConnection.Interface
{
    public interface ISqlConnection
    {
        static ISqlConnection Instance;

        IDbConnection CreateConnection(string dataSource, string userId, string password, string databaseName);

        void CloseConnection(IDbConnection connection);
    }
}
