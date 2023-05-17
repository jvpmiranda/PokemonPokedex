using System.Data;

namespace SqlServerDataBaseConnection.Interface
{
    public interface ISqlConnection
    {
        IDbConnection CreateConnection();
        void CloseConnection(IDbConnection connection);
    }
}
