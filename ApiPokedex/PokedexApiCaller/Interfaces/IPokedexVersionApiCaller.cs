using ApiPokedex.Contract.v1.In;
using PokedexApiCaller.Contract.v1.Out;

namespace PokedexApiCaller.Interfaces
{
    public interface IPokedexVersionApiCaller : IRequiresAuthentication
    {
        Task Delete(int versionId);
        Task<OutGetPokedexVersion> GetVersion(int? versionId);
        Task Post(InPokedexVersion pokedexVersion);
        Task Put(InPokedexVersion pokedexVersion);
    }
}