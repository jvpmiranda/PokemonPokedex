using Microsoft.Extensions.DependencyInjection;
using PokedexDataAccess.Factory;

namespace PokedexDataAccess.Extensions.Mongo;

public static class FactoryDbConnectionExtensions
{
    public static void AddFactoryDbConnection(this IServiceCollection service, string connectionString)
    {
        service.AddSingleton<IFactoryDbConnection, FactoryDbConnection> (x =>
        {
            return new FactoryDbConnection(connectionString);
        });
    }
}
