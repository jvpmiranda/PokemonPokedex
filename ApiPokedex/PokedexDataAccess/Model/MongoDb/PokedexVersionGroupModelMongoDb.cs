using MongoDB.Bson.Serialization.Attributes;

namespace PokedexDataAccess.Model.MongoDb;

public class PokedexVersionGroupModelMongoDb
{
    [BsonId]
    public int GroupId { get; set; }

    public string VersionName { get; set; }

    public int GenerationNumber { get; set; }

}