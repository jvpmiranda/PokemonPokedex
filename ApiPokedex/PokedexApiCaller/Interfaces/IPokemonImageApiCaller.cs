using PokedexApiCaller.Contract.v1.Out;

namespace PokedexApiCaller.Interfaces
{
    public interface IPokemonImageApiCaller : IRequiresAuthentication
    {
        Task<OutImage> GetImage(int pokemonId);
    }
}