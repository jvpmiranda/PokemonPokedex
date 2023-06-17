using ApiPokedex.Contract.v1.Out;
using ApiPokedex.Contract.v1.In;
using AutoMapper;
using PokedexModels.Model;

namespace ApiPokedex.Mapper
{
    public class ContractToDomain : Profile
    {
        public ContractToDomain()
        {
            CreateMap<InPokemon, PokemonModel>();

            CreateMap<InPokedexVersion, PokedexVersionModel>();
        }
    }
}
