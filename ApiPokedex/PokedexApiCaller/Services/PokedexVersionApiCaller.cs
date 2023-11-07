using PokedexApiCaller.Contract;
using PokedexApiCaller.Contract.v1.In;
using PokedexApiCaller.Contract.v1.Out;
using PokedexApiCaller.Factory;
using PokedexApiCaller.Interfaces;

namespace PokedexApiCaller.Services;

public class PokedexVersionApiCaller : IPokedexVersionApiCaller
{
    private HttpClient _client;
    private readonly FactoryHttpClient _factory;

    public Authentication Auth { get; set; }

    public PokedexVersionApiCaller(FactoryHttpClient factory)
    {
        _factory = factory;
        _client = _factory.Create();
    }

    public async Task<OutGetPokedexVersion> GetVersion(int? versionId)
    {
        _client.SetAuthentication(Auth);
        HttpResponseMessage response = await _client.GetAsync($"api/v1/PokedexVersion/GetVersion/{versionId}").ConfigureAwait(false);
        if (response.IsSuccessStatusCode)
            return await response.Content.ReadAsAsync<OutGetPokedexVersion>();

        return new OutGetPokedexVersion();
    }

    public async Task Post(InPokedexVersion pokedexVersion)
    {
        _client.SetAuthentication(Auth);
        await _client.PostAsJsonAsync($"api/v1/PokedexVersion/Post", pokedexVersion).ConfigureAwait(false);
    }

    public async Task Put(InPokedexVersion pokedexVersion)
    {
        _client.SetAuthentication(Auth);
        await _client.PutAsJsonAsync($"api/v1/PokedexVersion/Put", pokedexVersion).ConfigureAwait(false);
    }

    public async Task Delete(int versionId)
    {
        _client.SetAuthentication(Auth);
        await _client.DeleteAsync($"api/v1/PokedexVersion/Delete/{versionId}").ConfigureAwait(false);
    }
}