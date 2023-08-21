using PokedexApiCaller.Contract;
using PokedexApiCaller.Contract.v1.In;
using PokedexApiCaller.Contract.v1.Out;
using PokedexApiCaller.Factory;
using PokedexApiCaller.Interfaces;

namespace PokedexApiCaller.Services;

public class PokemonApiCaller : IPokemonApiCaller
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

    public async Task<OutPokemon> GetInfo(int pokemonId, int versionId)
    {
        HttpClient client = HttpClientPokemonApiFactory.Create(_baseUrlApi, Auth);
        HttpResponseMessage response = await client.GetAsync($"api/v1/Pokemon/GetInfo/{pokemonId}/{versionId}");
        if (response.IsSuccessStatusCode)
            return await response.Content.ReadAsAsync<OutPokemon>();

        return new OutPokemon();
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
        try
        {
            HttpClient client = HttpClientPokemonApiFactory.Create(_baseUrlApi, Auth);
            await client.DeleteAsync($"api/v1/Pokemon/Delete/{pokemonId}");
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}