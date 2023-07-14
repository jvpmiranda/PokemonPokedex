using MongoDB.Bson.Serialization.Attributes;

namespace PokedexDataAccess.Model.MongoDb;

public class TypeModelMongoDb
{
    [BsonId]
    public int Id { get; set; }

    public string Name { get; set; }
}