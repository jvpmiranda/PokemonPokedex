using PokedexModels.Model;

namespace PokedexDataAccess.Interfaces;

public interface IPokedexDataAccessService
{
    Task Delete(int pokemonId);
    Task<IEnumerable<PokemonModel>> GetBasicPokemon(int? pokemonId);
    Task<PokemonModel> GetPokemon(int pokemonId);
    Task<PokemonLineModel> GetPokemonLine(int pokemonId, int versionId);
    Task Insert(PokemonModel pokemon);
    Task Update(PokemonModel pokemon);
}