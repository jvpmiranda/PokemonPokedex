using ApiPokedex.Contract.In;
using ApiPokedex.Contract.Out;
using PokedexEF.Model;
using PokedexModels.Model;

namespace ApiPokedex.Mapper;

internal static class MapperPokedex
{
    internal static PokedexVersionModel ConvertToModel(InPokedexVersion pokedexVersion)
    {
        PokedexVersionModel pok = new PokedexVersionModel()
        {
            Id = pokedexVersion.Id,
            Name = pokedexVersion.Name,
            VersionGroup = new PokedexVersionGroupModel() { Id = pokedexVersion.VersionGroupId }
        };
        return pok;
    }

    internal static PokemonModel ConvertToModel(InPokemon pokemon)
    {
        PokemonModel pok = new PokemonModel()
        {
            Id = pokemon.Id,
            Identifier = pokemon.Name
        };
        return pok;
    }

    internal static IEnumerable<OutBasicInfoPokemon> ConvertToEnumerableOutBasicInfoPokemon(IEnumerable<PokemonModel> pokemons)
    {
        foreach (var pok in pokemons)
            yield return ConvertToOutBasicInfoPokemon(pok);
    }

    internal static OutBasicInfoPokemon ConvertToOutBasicInfoPokemon(PokemonModel pokemon)
    {
        OutBasicInfoPokemon pok = new OutBasicInfoPokemon()
        {
            Id = pokemon.Id,
            Name = pokemon.Identifier,
            Height = pokemon.Height,
            Weight = pokemon.Weight,
            EvolvesFromSpeciesId = pokemon.EvolvesFrom,
            GenerationId = pokemon.GenerationNumber
        };
        return pok;
    }

    internal static OutPokemon ConvertToOutPokemon(PokemonModel pokemon)
    {
        OutPokemon pok = new OutPokemon()
        {
            Id = pokemon.Id,
            Name = pokemon.Identifier,
            Height = pokemon.Height,
            Weight = pokemon.Weight,
            EvolvesFromSpeciesId = pokemon.EvolvesFrom,
            GenerationId = pokemon.GenerationNumber,
            EvolvesTo = pokemon.EvolvesTo,
            Types = ConvertToEnumerableOutTypeOfPokemon(pokemon.Types),
            Description = pokemon.Description
        };
        return pok;
    }

    internal static IEnumerable<OutTypeOfPokemon> ConvertToEnumerableOutTypeOfPokemon(IEnumerable<TypeModel> types)
    {
        foreach (var type in types)
            yield return ConvertToOutTypeOfPokemon(type);
    }

    internal static OutTypeOfPokemon ConvertToOutTypeOfPokemon(TypeModel type)
    {
        return new OutTypeOfPokemon()
        {
            Id = type.Id,
            Name = type.Name
        };
    }

    internal static OutFullPokemon ConvertToOutFullPokemon(PokemonModelFull pokemon)
    {
        OutFullPokemon result = new OutFullPokemon();
        ConvertToOutBasicPokemon(pokemon.Pokemon, result);
        List<OutEvolutionPokemon> evolvedPokemon = new List<OutEvolutionPokemon>();

        if (pokemon.EvolvesTo?.Count > 0)
            pokemon.EvolvesTo.ForEach(e => evolvedPokemon.Add(ConvertToOutEvolutionPokemon(e)));
        result.EvolvesTo = evolvedPokemon;
        
        if (pokemon.EvolvesFrom != null)
            result.EvolvesFrom = ConvertToOutPreEvolutionPokemon(pokemon.EvolvesFrom);

        return result;
    }

    internal static OutEvolutionPokemon ConvertToOutEvolutionPokemon(PokemonModelFull pokemon)
    {
        OutEvolutionPokemon result = new OutEvolutionPokemon();
        ConvertToOutBasicPokemon(pokemon.Pokemon, result);
        result.EvolvesTo = new List<OutEvolutionPokemon>();
        pokemon.EvolvesTo.ForEach(e => result.EvolvesTo.Add(ConvertToOutEvolutionPokemon(e)));
        return result;
    }

    internal static OutPreEvolutionPokemon ConvertToOutPreEvolutionPokemon(PokemonModelFull pokemon)
    {
        OutPreEvolutionPokemon result = new OutPreEvolutionPokemon();
        ConvertToOutBasicPokemon(pokemon.Pokemon, result);
        if (pokemon.EvolvesFrom != null)
            result.EvolvesFrom = ConvertToOutPreEvolutionPokemon(pokemon.EvolvesFrom);
        return result;
    }

    internal static void ConvertToOutBasicPokemon(PokemonModel pokemonModel, OutBasicPokemon pokemon)
    {
        pokemon.Id = pokemonModel.Id;
        pokemon.Name = pokemonModel.Identifier;
        pokemon.Height = pokemonModel.Height;
        pokemon.Weight = pokemonModel.Weight;
        pokemon.GenerationNumber = pokemonModel.GenerationNumber;
        pokemon.Description = pokemonModel.Description;
        pokemon.Types = ConvertToEnumerableOutTypeOfPokemon(pokemonModel.Types);

    }

    internal static IEnumerable<OutPokedexVersion> ConvertToEnumerableOutVersion(IEnumerable<PokedexVersionModel> versions)
    {
        foreach (var version in versions)
            yield return ConvertToOutVersion(version);
    }

    internal static OutPokedexVersion ConvertToOutVersion(PokedexVersionModel version)
    {
        return new OutPokedexVersion()
        {
            Id = version.Id,
            Name = version.Name,
            GenerationNumber = version.VersionGroup.GenerationNumber,
            VersionName = version.VersionGroup.VersionName
        };
    }
}
