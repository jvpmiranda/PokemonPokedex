using PokedexApiCaller.Contract;

namespace PokedexApiCaller.Interfaces
{
    public interface IAuthCallerApiCaller
    {
        Task<Authentication> GetToken(string name);
    }
}