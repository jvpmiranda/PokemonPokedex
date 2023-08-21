using PokedexApiCaller.Contract.v1.In;
using PokedexApiCaller.Contract.v1.Out;

namespace PokedexApiCaller.Interfaces
{
    public interface IPokemonImageApiCaller : IRequiresAuthentication
    {
        Task<OutImage> GetImage(int pokemonId);
        Task Delete(int pokemonId);
        Task Post(InImage image);
        Task Put(InImage image);
    }
}