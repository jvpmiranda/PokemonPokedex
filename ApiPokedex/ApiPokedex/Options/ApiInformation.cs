using PokedexDataAccess.DataAccess.Dapper;
using PokedexDataAccess.DataAccess.MongoDb;
using PokedexDataAccess.Extensions.Mongo;
using PokedexDataAccess.Interfaces;
using PokedexServices.Interfaces;

namespace ApiPokedex.Options;

public static class Configuration
{
    public const int ClientId = 13031995;

    public static void AddServicesByTypeOfConnection(this WebApplicationBuilder builder)
    {
        var typeOfConnection = builder.Configuration.GetValue<string>("TypeOfDataBaseConnection");
        switch (typeOfConnection)
        {
            case "DAPPER":
                builder.AddServicesDapper();
                break;
            case "MONGODB":
                builder.AddServicesMongoDb();
                break;
        }
    }

    private static void AddServicesDapper(this WebApplicationBuilder builder)
    {
        string connectionString = builder.Configuration.GetConnectionString("PokedexSQLServer")!;
        builder.Services.AddFactoryDbConnection(connectionString);
        builder.Services.AddTransient<IImageDataAccess, ImageDapper>();
        builder.Services.AddTransient<IPokedexVersionDataAccess, PokedexVersionDapper>();
        builder.Services.AddTransient<IPokedexVersionGroupDataAccess, PokedexVersionGroupDapper>();
        builder.Services.AddTransient<IPokemonDataAccess, PokemonDapper>();
        builder.Services.AddTransient<IPokemonTypesDataAccess, PokemonTypesDapper>();
        builder.Services.AddTransient<IPokemonDescriptionsDataAccess, PokemonDescriptionsDapper>();
    }

    private static void AddServicesMongoDb(this WebApplicationBuilder builder)
    {
        string connectionString = builder.Configuration.GetConnectionString("PokedexMongoDb")!;
        builder.Services.AddMongoConnection(connectionString);
        builder.Services.AddTransient<IImageDataAccess, ImageMongoDb>();
        builder.Services.AddTransient<IPokemonDataAccess, PokemonMongoDb>();
        builder.Services.AddTransient<IPokedexVersionDataAccess, PokedexVersionMongoDb>();
        builder.Services.AddTransient<IPokedexVersionGroupDataAccess, PokedexVersionGroupMongoDb>();
        builder.Services.AddTransient<IPokemonTypesDataAccess, PokemonTypesMongoDb>();
        builder.Services.AddTransient<IPokemonDescriptionsDataAccess, PokemonDescriptionsMongoDb>();
    }
}
