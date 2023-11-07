using System.Data;
using System.Data.SqlClient;

namespace PokedexDataAccess.Factory
{
    public class FactoryDbConnection
    {
        private readonly string _connectionString;

        public FactoryDbConnection(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IDbConnection CreateConnection() => new SqlConnection(_connectionString);
    }
}
