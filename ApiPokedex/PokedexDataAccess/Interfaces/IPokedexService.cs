using PokedexModels.Model;

namespace PokedexDataAccess.Interfaces;

public interface IPokedexDataAccessService
{
    void Delete(int pokemonId);
    IEnumerable<PokemonModel> GetPokemon(string pokemonKey);
    PokemonModel GetPokemon(int pokemonId, int versionId);
    void Insert(PokemonModel pokemon);
    void Update(PokemonModel pokemon);
}