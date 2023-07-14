using ApiPokedex.Contract.v1.In;
using ApiPokedex.Contract.v1.Out;
using AutoMapper;
using PokedexModels.Model;
using System.Text.RegularExpressions;

namespace ApiPokedex.Mapper;

public class ContractToDomain : Profile
{
    public ContractToDomain()
    {
        CreateMap<InPokemon, PokemonModel>()
            .ForMember(p => p.EvolvesFrom, m =>
            {
                m.PreCondition(p => p.EvolvesFromId.HasValue);
                m.MapFrom((orig) => new PokemonModel() { Id = orig.EvolvesFromId.Value });
            })
            .ForMember(p => p.EvolvesTo, m =>
            {
                m.PreCondition(p => p.EvolvesToId?.Any() ?? false);
                m.MapFrom((orig) => orig.EvolvesToId.Select(e => new PokemonModel() { Id = e }));
            });

        CreateMap<InType, TypeModel>();
        CreateMap<InPokemonVersion, PokemonVersionModel>();

        CreateMap<InPokedexVersion, PokedexVersionModel>()
            .ForMember(p => p.VersionGroup, m =>
            {
                m.MapFrom(orig => new PokedexVersionGroupModel()
                {
                    GroupId = orig.GroupId,
                    GenerationNumber = orig.GenerationNumber,
                    VersionName = orig.VersionName
                });
            });

        CreateMap<InImage, ImageModel>();
    }
}
