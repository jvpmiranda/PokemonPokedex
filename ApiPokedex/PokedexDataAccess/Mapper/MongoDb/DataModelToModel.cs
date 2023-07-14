using AutoMapper;
using PokedexDataAccess.Model.MongoDb;
using PokedexModels.Model;

namespace PokedexDataAccess.Mapper.MongoDb;

public class DataModelToModel : Profile
{
    public DataModelToModel()
    {
        CreateMap<PokemonModelMongoDb, PokemonModel>();
        CreateMap<ImageModelMongoDb, PokemonModel>();
        CreateMap<PokedexVersionModelMongoDb, PokemonModel>();
        CreateMap<TypeModelMongoDb, PokemonModel>();
        CreateMap<PokemonVersionModelMongoDb, PokemonModel>();
    }
}
