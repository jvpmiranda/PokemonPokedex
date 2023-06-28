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

    public async Task<IEnumerable<PokemonModel>> GetBasicPokemon(int? pokemonId)
    {
        return await _dataAccess.GetBasicPokemon(pokemonId);
    }

    public async Task<PokemonModel> GetPokemon(int pokemonId)
    {
        return await _dataAccess.GetPokemon(pokemonId);
    }

    public async Task<PokemonLineModel> GetPokemonFullInfo(int pokemonId, int versionId)
    {
        PokemonLineModel pokemonFull = await _dataAccess.GetPokemonLine(pokemonId, versionId);

        if (pokemonFull.EvolvesFrom.HasValue)
            pokemonFull.PreEvolution = GetPreEvolutionCicle(pokemonFull.EvolvesFrom.Value, versionId);

        foreach (var id in pokemonFull.EvolvesTo)
            pokemonFull.Evolutions.Add(GetEvolutionCicle(id, versionId));

        return pokemonFull;
    }

    public async Task Insert(PokemonModel pokemon)
    {
        await _dataAccess.Insert(pokemon);
    }

    public async Task Update(PokemonModel pokemon)
    {
        await _dataAccess.Update(pokemon);
    }

    public async Task Delete(int pokemonId)
    {
        await _dataAccess.Delete(pokemonId);
    }

    private PokemonLineModel GetPreEvolutionCicle(int pokemonId, int versionId)
    {
        PokemonLineModel pokemonFull = _dataAccess.GetPokemonLine(pokemonId, versionId).Result;
        if (pokemonFull.EvolvesFrom.HasValue)
            pokemonFull.PreEvolution = GetPreEvolutionCicle(pokemonFull.EvolvesFrom.Value, versionId);

        return pokemonFull;
    }

    private PokemonLineModel GetEvolutionCicle(int pokemonId, int versionId)
    {
        PokemonLineModel pokemonFull = _dataAccess.GetPokemonLine(pokemonId, versionId).Result;

        foreach (var id in pokemonFull.EvolvesTo)
            pokemonFull.Evolutions.Add(GetEvolutionCicle(id, versionId));

        return pokemonFull;
    }
}
