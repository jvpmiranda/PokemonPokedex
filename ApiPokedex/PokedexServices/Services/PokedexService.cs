using PokedexDataAccess.Interfaces;
using PokedexModels.Model;
using PokedexServices.Interfaces;

namespace PokedexServices.Services;

public class PokedexService : IPokedexService
{
    protected readonly IPokedexDataAccessService _dataAccess;

    public PokedexService(IPokedexDataAccessService dataAccess)
    {
        _dataAccess = dataAccess;
    }

    public IEnumerable<PokemonModel> GetPokemon(string pokemonKey)
    {
        return _dataAccess.GetPokemon(pokemonKey);
    }

    public PokemonModel GetPokemon(int pokemonId, int versionId)
    {
        return _dataAccess.GetPokemon(pokemonId, versionId);
    }

    public PokemonModelFull GetPokemonFullInfo(int pokemonId, int versionId)
    {
        PokemonModelFull pokemonFull = new PokemonModelFull(GetPokemon(pokemonId, versionId));

        if (pokemonFull.EvolvesFrom.HasValue)
            pokemonFull.PreEvolution = GetPreEvolutionCicle(pokemonFull.EvolvesFrom.Value, versionId);

        foreach (var id in pokemonFull.EvolvesTo)
            pokemonFull.Evolutions.Add(GetEvolutionCicle(id, versionId));

        return pokemonFull;
    }

    private PokemonModelFull GetPreEvolutionCicle(int pokemonId, int versionId)
    {
        PokemonModelFull pokemonFull = new PokemonModelFull(GetPokemon(pokemonId, versionId));
        if (pokemonFull.EvolvesFrom.HasValue)
            pokemonFull.PreEvolution = GetPreEvolutionCicle(pokemonFull.EvolvesFrom.Value, versionId);

        return pokemonFull;
    }

    private PokemonModelFull GetEvolutionCicle(int evolution, int versionId)
    {
        PokemonModelFull pokemonFull = new PokemonModelFull(GetPokemon(evolution, versionId));

        foreach (var id in pokemonFull.EvolvesTo)
            pokemonFull.Evolutions.Add(GetEvolutionCicle(id, versionId));

        return pokemonFull;
    }

    public void Insert(PokemonModel pokemon)
    {
        _dataAccess.Insert(pokemon);
    }

    public void Update(PokemonModel pokemon)
    {
        _dataAccess.Update(pokemon);
    }

    public void Delete(int pokemonId)
    {
        _dataAccess.Delete(pokemonId);
    }
}
