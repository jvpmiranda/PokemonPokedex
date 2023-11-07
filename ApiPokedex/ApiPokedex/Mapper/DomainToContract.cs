using ApiPokedex.Contract.v1.Out;
using AutoMapper;
using PokedexModels.Model;

namespace ApiPokedex.Mapper;

public class DomainToContract : Profile
{
    public DomainToContract()
    {
        PokemonGetBasicInfo();
        PokemonGetInfo();
        PokedexVersionGetVersion();
        PokemonImageGetImage();
    }

    private void PokemonGetBasicInfo() => CreateMap<PokemonModel, OutBasicInfoPokemon>();

    private void PokemonGetInfo()
    {
        CreateMap<PokemonModel, OutPokemon>();
        CreateMap<PokemonModel, OutEvolutionPokemon>();
        CreateMap<PokemonModel, OutPreEvolutionPokemon>();
        CreateMap<TypeModel, OutTypeOfPokemon>();
        CreateMap<PokemonVersionModel, OutPokemonVersion>();
        CreateMap<PokemonVersionGroupModel, OutPokemonVersion>();
    }

    private void PokedexVersionGetVersion() => CreateMap<PokedexVersionModel, OutPokedexVersion>()
        .ForMember(p => p.GroupId, m => m.MapFrom(orig => orig.VersionGroup.GroupId))
        .ForMember(p => p.GenerationNumber, m => m.MapFrom(orig => orig.VersionGroup.GenerationNumber))
        .ForMember(p => p.VersionName, m => m.MapFrom(orig => orig.VersionGroup.VersionName));
    private void PokemonImageGetImage() => CreateMap<ImageModel, OutImage>();
}
