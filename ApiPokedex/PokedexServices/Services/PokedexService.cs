using PokedexDataAccess.Interfaces;
using PokedexEF.Model;
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
        PokemonModelFull pokemonFull = new PokemonModelFull();
        pokemonFull.Pokemon = _dataAccess.GetPokemon(pokemonId, versionId);

        if (pokemonFull.Pokemon.EvolvesFrom.HasValue)
            pokemonFull.EvolvesFrom = GetPreEvolutionCicle(pokemonFull.Pokemon.EvolvesFrom.Value, versionId);

        foreach (var id in pokemonFull.Pokemon.EvolvesTo)
            pokemonFull.EvolvesTo.Add(GetEvolutionCicle(versionId, id));

        return pokemonFull;
    }

    private PokemonModelFull GetPreEvolutionCicle(int pokemonId, int versionId)
    {
        PokemonModelFull pokemonFull = new PokemonModelFull();

        pokemonFull.Pokemon = _dataAccess.GetPokemon(pokemonId, versionId);
        if (pokemonFull.Pokemon.EvolvesFrom.HasValue)
            pokemonFull.EvolvesFrom = GetPreEvolutionCicle(pokemonFull.Pokemon.EvolvesFrom.Value, versionId);

        return pokemonFull;
    }

    private PokemonModelFull GetEvolutionCicle(int versionId, int evolutions)
    {
        PokemonModelFull pokemonFull = new PokemonModelFull();
        
        pokemonFull.Pokemon = _dataAccess.GetPokemon(evolutions, versionId);
        foreach (var id in pokemonFull.Pokemon.EvolvesTo)
            pokemonFull.EvolvesTo.Add(GetEvolutionCicle(versionId, id));

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
