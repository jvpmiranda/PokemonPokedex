using PokedexApiCaller.Contract;

namespace PokedexApiCaller.Interfaces
{
    public interface IAuthApiCaller
    {
        Task<Authentication> GetToken(string name);
    }
}