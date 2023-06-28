using ApiPokedex.Contract;
using ApiPokedex.Contract.v1.In;
using PokedexApiCaller.Contract;
using PokedexApiCaller.Contract.v1.Out;
using PokedexApiCaller.Factory;
using PokedexApiCaller.Interfaces;
using System;

namespace PokedexApiCaller.Services;

public class PokedexVersionApiCaller : IPokedexVersionApiCaller
{
    private readonly string _baseUrlApi;
    public Authentication Auth { get; set; }

    public PokedexVersionApiCaller(string baseUrlApi) => _baseUrlApi = baseUrlApi;

    public async Task<OutGetPokedexVersion> GetVersion(int? versionId)
    {
        HttpClient client = HttpClientPokemonApiFactory.Create(_baseUrlApi, Auth);
        HttpResponseMessage response = await client.GetAsync($"api/v1/PokedexVersion/GetVersion/{versionId}");
        if (response.IsSuccessStatusCode)
            return await response.Content.ReadAsAsync<OutGetPokedexVersion>();

        return new OutGetPokedexVersion();
    }

    public async Task Post(InPokedexVersion pokedexVersion)
    {
        HttpClient client = HttpClientPokemonApiFactory.Create(_baseUrlApi, Auth);
        await client.PostAsJsonAsync($"api/v1/PokedexVersion/Post", pokedexVersion);
    }

    public async Task Put(InPokedexVersion pokedexVersion)
    {
        HttpClient client = HttpClientPokemonApiFactory.Create(_baseUrlApi, Auth);
        await client.PutAsJsonAsync($"api/v1/PokedexVersion/Put", pokedexVersion);
    }

    public async Task Delete(int versionId)
    {
        HttpClient client = HttpClientPokemonApiFactory.Create(_baseUrlApi, Auth);
        await client.DeleteAsync($"api/v1/PokedexVersion/Delete/{versionId}");
    }
}