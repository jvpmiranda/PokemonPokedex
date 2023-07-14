using AutoMapper;
using PokedexDataAccess.Model.MongoDb;
using PokedexModels.Model;

namespace PokedexDataAccess.Mapper.MongoDb;

public class ModelToDataModel : Profile
{
    public ModelToDataModel()
    {
        CreateMap<PokemonModel, PokemonModelMongoDb>();
        CreateMap<ImageModel, PokemonModelMongoDb>();
        CreateMap<PokedexVersionModel, PokemonModelMongoDb>();
        CreateMap<TypeModel, PokemonModelMongoDb>();
        CreateMap<PokemonVersionModel, PokemonModelMongoDb>();
    }
}
