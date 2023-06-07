using PokedexServices.Model;

namespace PokedexServices.Interfaces
{
    public interface IPokedexService
    {
        void Delete(int pokemonId);
        IEnumerable<PokemonModel> GetPokemon(string pokemonKey);
        void Insert(PokemonModel pokemon);
        void Update(PokemonModel pokemon);
    }
}