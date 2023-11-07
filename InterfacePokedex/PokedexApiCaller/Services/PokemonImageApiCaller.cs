using PokedexApiCaller.Contract;
using PokedexApiCaller.Contract.v1.In;
using PokedexApiCaller.Contract.v1.Out;
using PokedexApiCaller.Factory;
using PokedexApiCaller.Interfaces;

namespace PokedexApiCaller.Services;

public class PokemonImageApiCaller : IPokemonImageApiCaller
{
    private HttpClient _client;
    private FactoryHttpClient _factory;

    public Authentication Auth { get; set; }

    public PokemonImageApiCaller(FactoryHttpClient factory)
    {
        _factory = factory;
        _client = _factory.Create();
    }
    public async Task<OutImage> GetImage(int pokemonId)
    {
        _client.SetAuthentication(Auth);
        HttpResponseMessage response = await _client.GetAsync($"api/v1/PokemonImage/GetImage/{pokemonId}");
        if (response.IsSuccessStatusCode)
            return await response.Content.ReadAsAsync<OutImage>();

        return new OutImage();
    }

    public async Task Post(InImage image)
    {
        _client.SetAuthentication(Auth);
        await _client.PostAsJsonAsync($"api/v1/PokemonImage/Post", image);
    }

    public async Task Put(InImage image)
    {
        _client.SetAuthentication(Auth);
        await _client.PutAsJsonAsync($"api/v1/PokemonImage/Put", image);
    }

    public async Task Delete(int pokemonId)
    {
        _client.SetAuthentication(Auth);
        await _client.DeleteAsync($"api/v1/PokemonImage/Delete/{pokemonId}");
    }
}