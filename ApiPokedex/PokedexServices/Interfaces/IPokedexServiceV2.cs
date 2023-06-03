using PokedexServices.Model;

namespace PokedexServices.Interfaces
{
    public interface IPokedexServiceV2 : IPokedexService
    {
        PokemonModel GetPokemon(int pokemonId, int versionId);
    }
}