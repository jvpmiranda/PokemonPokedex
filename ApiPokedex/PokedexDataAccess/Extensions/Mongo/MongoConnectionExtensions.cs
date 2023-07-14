using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace PokedexDataAccess.Extensions.Mongo;

public static class MongoConnectionExtensions
{
    public static void AddMongoConnection(this IServiceCollection service, string connectionString)
    {
        service.AddSingleton(x =>
        {
            var _database = new MongoClient(connectionString);
            var _databaseName = MongoUrl.Create(connectionString).DatabaseName;
            return _database.GetDatabase(_databaseName);
        });
    }
}
