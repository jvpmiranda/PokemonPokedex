using PokedexModels.Model;

namespace PokedexServices.Interfaces;

public interface IPokemonService
{
    Task Delete(int pokemonId);
    Task<IEnumerable<PokemonModel>> GetBasicPokemon(int? pokemonId);
    Task<PokemonModel> GetPokemon(int pokemonId, int versionId);
    Task Insert(PokemonModel pokemon);
    Task Update(PokemonModel pokemon);
}