using PokedexServices.Model;

namespace PokedexServices.Interfaces
{
    public interface IPokedexService
    {
        void Delete(int pokemonId);
        IEnumerable<PokemonModel> GetPokemon();
        PokemonModel GetPokemon(int pokemonId);
        void Insert(PokemonModel pokemon);
        void Update(PokemonModel pokemon);
    }
}