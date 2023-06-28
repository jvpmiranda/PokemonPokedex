using PokedexModels.Model;

namespace PokedexServices.Interfaces;

public interface IPokedexService
{
    Task Delete(int pokemonId);
    Task<IEnumerable<PokemonModel>> GetBasicPokemon(int? pokemonId);
    Task<PokemonModel> GetPokemon(int pokemonId);
    Task<PokemonLineModel> GetPokemonFullInfo(int pokemonId, int versionId);
    Task Insert(PokemonModel pokemon);
    Task Update(PokemonModel pokemon);
}