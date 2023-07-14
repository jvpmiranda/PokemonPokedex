using MongoDB.Bson.Serialization.Attributes;

namespace PokedexDataAccess.Model.MongoDb;

public class PokedexVersionModelMongoDb
{
    [BsonId]
    public int Id { get; set; }

    public string Name { get; set; }

    public int GroupId { get; set; }

    public string VersionName { get; set; }

    public int GenerationNumber { get; set; }

}