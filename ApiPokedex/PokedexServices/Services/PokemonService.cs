using PokedexDataAccess.Interfaces;
using PokedexModels.Model;
using PokedexServices.Interfaces;

namespace PokedexServices.Services;

public class PokemonService : IPokemonService
{
    private readonly IPokemonDataAccess _pokemon;
    private readonly IPokemonTypesDataAccess _typeOfPokemon;
    private readonly IPokemonDescriptionsDataAccess _pokemonDescription;

    public PokemonService(IPokemonDataAccess dataAccess, IPokemonTypesDataAccess typeOfPokemon, IPokemonDescriptionsDataAccess pokemonDescription)
    {
        _pokemon = dataAccess;
        _typeOfPokemon = typeOfPokemon;
        _pokemonDescription = pokemonDescription;
    }

    public async Task<IEnumerable<PokemonModel>> GetBasicPokemon(int? pokemonId)
    {
        return await _pokemon.GetBasicPokemon(pokemonId);
    }

    public async Task<IEnumerable<PokemonModel>> GetPagedBasicPokemon(int page, int quantity)
    {
        var startIndex = (page * quantity - quantity) + 1;
        return await _pokemon.GetPagedBasicPokemon(startIndex, quantity);
    }

    public async Task<long> GetNumberOfPokemons()
    {
        return await _pokemon.GetNumberOfPokemons();
    }

    public async Task<PokemonModel> GetPokemon(int pokemonId, int versionId)
    {
        PokemonModel pokemonFull = await _pokemon.GetPokemon(pokemonId, versionId);
        if (pokemonFull.Id == 0)
            return pokemonFull;

        if (pokemonFull.EvolvesFrom != null)
            pokemonFull.EvolvesFrom = GetPreEvolutionCicle(pokemonFull.EvolvesFrom.Id, versionId);

        var lsPokemon = new List<PokemonModel>();
        foreach (var pok in pokemonFull.EvolvesTo)
        {
            var poke = GetEvolutionCicle(pok.Id, versionId);
            if (poke.Id > 0)
                lsPokemon.Add(poke);
        }
        pokemonFull.EvolvesTo = lsPokemon;

        return pokemonFull;
    }

    public async Task Insert(PokemonModel pokemon)
    {
        var pok = await GetBasicPokemon(pokemon.Id);
        //await _pokemon.UseTransaction(transaction =>
        //{
        //_typeOfPokemon.InsertInTransaction(pokemon.Types, transaction);
        //if (pok.Any())
        //{
        //    foreach (var version in pokemon.Versions)
        //    {
        //        var ver = _pokemonDescription.GetInTransaction(pokemon.Id, version.VersionId, transaction);
        //        if (ver.Result.Any())
        //            _pokemonDescription.InsertInTransaction(pokemon.Id, version, transaction);
        //    }
        //}
        //else
        //    _pokemon.InsertInTransaction(pokemon, transaction);
        //return true;
        //});
        await _typeOfPokemon.Upsert(pokemon.Types);
        if (pok.Any())
        {
            foreach (var version in pokemon.Versions)
            {
                var ver = await _pokemonDescription.Get(pokemon.Id, version.VersionId);
                if (!ver.Any())
                    await _pokemonDescription.Upsert(pokemon.Id, version);
            }
        }
        else
            await _pokemon.Insert(pokemon);

    }

    public async Task Update(PokemonModel pokemon)
    {
        await _pokemon.Update(pokemon);
    }

    public async Task Delete(int pokemonId)
    {
        await _pokemon.Delete(pokemonId);
    }

    private PokemonModel GetPreEvolutionCicle(int pokemonId, int versionId)
    {
        PokemonModel pokemonFull = _pokemon.GetPokemon(pokemonId, versionId).Result;
        if (pokemonFull.EvolvesFrom != null)
            pokemonFull.EvolvesFrom = GetPreEvolutionCicle(pokemonFull.EvolvesFrom.Id, versionId);

        return pokemonFull;
    }

    private PokemonModel GetEvolutionCicle(int pokemonId, int versionId)
    {
        PokemonModel pokemonFull = _pokemon.GetPokemon(pokemonId, versionId).Result;

        var lsPokemon = new List<PokemonModel>();
        foreach (var pok in pokemonFull.EvolvesTo)
            lsPokemon.Add(GetEvolutionCicle(pok.Id, versionId));
        pokemonFull.EvolvesTo = lsPokemon;

        return pokemonFull;
    }
}
