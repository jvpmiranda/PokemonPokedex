using ApiPokedex.Contract.v1.In;
using PokedexApiCaller.Contract;
using PokedexApiCaller.Contract.v1.Out;
using PokedexApiCaller.Factory;

namespace PokedexApiCaller.Caller;

public class PokemonApiCaller
{
    private readonly string _baseUrlApi;
    public Authentication Auth { get; set; }

    public PokemonApiCaller(string baseUrlApi) => _baseUrlApi = baseUrlApi;

    public async Task<OutGetBasicInfoPokemon> GetBasicInfo(int pokemonId)
    {
        HttpClient client = HttpClientPokemonApiFactory.Create(_baseUrlApi, Auth);
        HttpResponseMessage response = await client.GetAsync($"api/v1/Pokemon/GetBasicInfo/{pokemonId}/");
        if (response.IsSuccessStatusCode)
            return await response.Content.ReadAsAsync<OutGetBasicInfoPokemon>();

        return new OutGetBasicInfoPokemon();
    }

    public async Task<OutPokemon> GetInfo(int pokemonId)
    {
        HttpClient client = HttpClientPokemonApiFactory.Create(_baseUrlApi, Auth);
        HttpResponseMessage response = await client.GetAsync($"api/v1/Pokemon/GetInfo/{pokemonId}/");
        if (response.IsSuccessStatusCode)
            return await response.Content.ReadAsAsync<OutPokemon>();

        return new OutPokemon();
    }

    public async Task<OutFullPokemon> GetFullInfo(int pokemonId, int versionId)
    {
        HttpClient client = HttpClientPokemonApiFactory.Create(_baseUrlApi, Auth);
        HttpResponseMessage response = await client.GetAsync($"api/v1/Pokemon/GetFullInfo/{pokemonId}/{versionId}");
        if (response.IsSuccessStatusCode)
            return await response.Content.ReadAsAsync<OutFullPokemon>();

        return new OutFullPokemon();
    }

    public async Task Post(InPokemon pokemon)
    {
        HttpClient client = HttpClientPokemonApiFactory.Create(_baseUrlApi, Auth);
        await client.PostAsJsonAsync($"api/v1/Pokemon/Post", pokemon);
    }

    public async Task Put(InPokemon pokemon)
    {
        HttpClient client = HttpClientPokemonApiFactory.Create(_baseUrlApi, Auth);
        await client.PutAsJsonAsync($"api/v1/Pokemon/Put", pokemon);
    }

    public async Task Delete(int pokemonId)
    {
        HttpClient client = HttpClientPokemonApiFactory.Create(_baseUrlApi, Auth);
        await client.DeleteAsync($"api/v1/Pokemon/Delete/{pokemonId}");
    }
}