using PokedexDataAccess.Interfaces.Infrastructure;
using PokedexModels.Model;

namespace PokedexServices.Interfaces;

public interface IPokemonDescriptionsDataAccess : IDataAccessTransaction
{
    Task Delete(int pokemonId, int versionId);
    Task DeleteInTransaction(int pokemonId, int versionId, IDataAccessTransaction tran);
    Task<IEnumerable<PokemonVersionModel>> Get(int pokemonId, int? versionId);
    Task<IEnumerable<PokemonVersionModel>> GetInTransaction(int pokemonId, int?versionId, IDataAccessTransaction tran);
    Task Upsert(int pokemonId, PokemonVersionModel version);
    Task UpsertInTransaction(int pokemonId, PokemonVersionModel version, IDataAccessTransaction tran);
}