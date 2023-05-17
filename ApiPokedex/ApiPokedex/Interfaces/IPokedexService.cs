using ApiPokedex.Models;

namespace ApiPokedex.Interfaces
{
    public interface IPokedexService
    {
        void Delete(int pokemonId);
        IEnumerable<Pokemon> GetPokemon();
        Pokemon GetPokemon(int pokemonId);
        void Insert(Pokemon pokemon);
        void Update(Pokemon pokemon);
    }
}