using PokedexDataAccess.Interfaces.Infrastructure;
using System.Data;

namespace PokedexDataAccess.Factory
{
    public interface IFactoryDbConnection
    {
        IDbConnection CreateConnection();
    }
}