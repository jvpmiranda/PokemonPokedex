using MongoDB.Bson.Serialization.Attributes;

namespace PokedexDataAccess.Model.MongoDb;

public class PokemonVersionModelMongoDb
{
    [BsonId]
    public int VersionId { get; set; }

    public string Description { get; set; }

    public string VersionName { get; set; }

}
