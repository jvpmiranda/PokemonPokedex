using ApiPokedex.Contract.v1.Out;
using AutoMapper;
using PokedexModels.Model;

namespace ApiPokedex.Mapper
{
    public class DomainToContract : Profile
    {
        public DomainToContract()
        {
            CreateMap<PokemonModel, OutPokemon>();
            CreateMap<PokemonModel, OutBasicInfoPokemon>();

            CreateMap<PokemonLineModel, OutFullPokemon>();
            CreateMap<PokemonLineModel, OutEvolutionPokemon>();
            CreateMap<PokemonLineModel, OutPreEvolutionPokemon>();

            CreateMap<TypeModel, OutTypeOfPokemon>();
            CreateMap<PokemonVersionModel, OutGameVersion>();

            CreateMap<PokedexVersionModel, OutPokedexVersion>();
            
            CreateMap<ImageModel, OutImage>();
        }
    }
}
