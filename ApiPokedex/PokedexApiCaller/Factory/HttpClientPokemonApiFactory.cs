using PokedexApiCaller.Contract;
using System.Net.Http.Headers;

namespace PokedexApiCaller.Factory;

public static class HttpClientPokemonApiFactory
{
    public static HttpClient Create(string baseUrlApi)
    {
        HttpClient _client = new HttpClient();
        _client.BaseAddress = new Uri(baseUrlApi);
        _client.DefaultRequestHeaders.Accept.Clear();
        _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        return _client;
    }

    public static HttpClient Create(string baseUrlApi, Authentication auth)
    {
        ArgumentNullException.ThrowIfNull(auth, nameof(auth));

        HttpClient _client = Create(baseUrlApi);
        _client.DefaultRequestHeaders.Add("Authorization", $"Bearer {auth.Token}");

        return _client;
    }
}
