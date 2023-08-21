using PokedexApiCaller.Contract;
using PokedexApiCaller.Contract.v1.In;
using PokedexApiCaller.Contract.v1.Out;
using PokedexApiCaller.Factory;
using PokedexApiCaller.Interfaces;

namespace PokedexApiCaller.Services;

public class PokemonImageApiCaller : IPokemonImageApiCaller
{
    private readonly string _baseUrlApi;
    public Authentication Auth { get; set; }

    public PokemonImageApiCaller(string baseUrlApi) => _baseUrlApi = baseUrlApi;

    public async Task<OutImage> GetImage(int pokemonId)
    {
        HttpClient client = HttpClientPokemonApiFactory.Create(_baseUrlApi, Auth);
        HttpResponseMessage response = await client.GetAsync($"api/v1/PokemonImage/GetImage/{pokemonId}");
        if (response.IsSuccessStatusCode)
            return await response.Content.ReadAsAsync<OutImage>();

        return new OutImage();
    }

    public async Task Post(InImage image)
    {
        HttpClient client = HttpClientPokemonApiFactory.Create(_baseUrlApi, Auth);
        await client.PostAsJsonAsync($"api/v1/PokemonImage/Post", image);
    }

    public async Task Put(InImage image)
    {
        HttpClient client = HttpClientPokemonApiFactory.Create(_baseUrlApi, Auth);
        await client.PutAsJsonAsync($"api/v1/PokemonImage/Put", image);
    }

    public async Task Delete(int pokemonId)
    {
        HttpClient client = HttpClientPokemonApiFactory.Create(_baseUrlApi, Auth);
        await client.DeleteAsync($"api/v1/PokemonImage/Delete/{pokemonId}");
    }
}