using PokedexApiCaller.Contract;

namespace PokedexApiCaller.Interfaces
{
    public interface IRequiresAuthentication
    {
        Authentication Auth { get; set; }
    }
}
