using ApiPokedex.Contract.v1.In;
using PokedexApiCaller.Contract.v1.Out;

namespace PokedexApiCaller.Interfaces
{
    public interface IPokemonApiCaller : IRequiresAuthentication
    {
        Task Delete(int pokemonId);
        Task<OutGetBasicInfoPokemon> GetBasicInfo(int pokemonId);
        Task<OutFullPokemon> GetFullInfo(int pokemonId, int versionId);
        Task<OutPokemon> GetInfo(int pokemonId);
        Task Post(InPokemon pokemon);
        Task Put(InPokemon pokemon);
    }
}