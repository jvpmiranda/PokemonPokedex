using Microsoft.Extensions.Options;
using PokedexApiCaller.Config;
using PokedexApiCaller.Contract;
using System.Net.Http.Headers;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;

namespace PokedexApiCaller.Factory;

public class FactoryHttpClient
{
    private readonly PokedexSettings _pokedexSettings;

    public FactoryHttpClient(IOptions<PokedexSettings> pokedexSettings)
    {
        _pokedexSettings = pokedexSettings.Value;
    }

    public HttpClient Create()
    {
        HttpClient _client = new HttpClient();
        _client.BaseAddress = new Uri(_pokedexSettings.PokedexApiUrl);
        _client.DefaultRequestHeaders.Accept.Clear();
        _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        return _client;
    }
}
