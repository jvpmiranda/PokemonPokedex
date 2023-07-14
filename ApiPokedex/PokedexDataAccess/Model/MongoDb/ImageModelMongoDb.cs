using MongoDB.Bson.Serialization.Attributes;

namespace PokedexDataAccess.Model.MongoDb;

public class ImageModelMongoDb
{
    [BsonId]
    public int Id { get; set; }

    public byte[] Image { get; set; }
}