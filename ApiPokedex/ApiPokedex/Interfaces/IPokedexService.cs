using ApiPokedex.Model;

namespace ApiPokedex.Interfaces
{
    public interface IPokedexService
    {
        void Delete(int pokemonId);
        IEnumerable<PokemonPokedex> GetPokemon();
        PokemonPokedex GetPokemon(int pokemonId);
        void Insert(PokemonPokedex pokemon);
        void Update(PokemonPokedex pokemon);
    }
}