using MongoDB.Bson.Serialization.Attributes;

namespace PokedexDataAccess.Model.MongoDb;

public class PokemonModelMongoDb
{
    [BsonId]
    public int Id { get; set; }

    public string Name { get; set; }

    public int Height { get; set; }

    public double Weight { get; set; }

    public int GenerationNumber { get; set; }

    public int? EvolvesFrom { get; set; }

    public List<int>? EvolvesTo { get; set; }

    public List<TypeModelMongoDb> Types { get; set; }

    public List<PokemonVersionModelMongoDb> Versions { get; set; }

    public string ImageName { get; set; }
}
