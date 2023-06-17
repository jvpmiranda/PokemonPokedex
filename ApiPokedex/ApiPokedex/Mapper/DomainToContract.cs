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
            
            CreateMap<PokemonModelFull, OutFullPokemon>();
            CreateMap<PokemonModelFull, OutEvolutionPokemon>();
            CreateMap<PokemonModelFull, OutPreEvolutionPokemon>();

            CreateMap<TypeModel, OutTypeOfPokemon>();

            CreateMap<PokedexVersionModel, OutGetPokedexVersion>();
        }
    }
}
