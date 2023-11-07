using PokedexDataAccess.Interfaces.Infrastructure;
using PokedexModels.Model;

namespace PokedexDataAccess.Interfaces;

public interface IPokemonDataAccess : IDataAccessTransaction
{
    Task Delete(int pokemonId);
    Task DeleteInTransaction(int pokemonId, IDataAccessTransaction tran);
    Task<IEnumerable<PokemonModel>> GetBasicPokemon(int? pokemonId);
    Task<IEnumerable<PokemonModel>> GetBasicPokemonInTransaction(int? pokemonId, IDataAccessTransaction tran);
    Task<IEnumerable<PokemonModel>> GetPagedBasicPokemon(int start, int quantity);
    Task<IEnumerable<PokemonModel>> GetPagedBasicPokemonInTransaction(int start, int quantity, IDataAccessTransaction tran);
    Task<long> GetNumberOfPokemons();
    Task<long> GetNumberOfPokemonsInTransaction(IDataAccessTransaction tran);
    Task<PokemonModel> GetPokemon(int pokemonId, int versionId);
    Task<PokemonModel> GetPokemonInTransaction(int pokemonId, int versionId, IDataAccessTransaction tran);
    Task Insert(PokemonModel pokemon);
    Task InsertInTransaction(PokemonModel pokemon, IDataAccessTransaction tran);
    Task Update(PokemonModel pokemon);
    Task UpdateInTransaction(PokemonModel pokemon, IDataAccessTransaction tran);
}