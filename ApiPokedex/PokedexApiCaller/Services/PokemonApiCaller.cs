using PokedexApiCaller.Contract;
using PokedexApiCaller.Contract.v1.In;
using PokedexApiCaller.Contract.v1.Out;
using PokedexApiCaller.Factory;
using PokedexApiCaller.Interfaces;

namespace PokedexApiCaller.Services;

public class PokemonApiCaller : IPokemonApiCaller
{
    private HttpClient _client;
    private readonly FactoryHttpClient _factory;
    public Authentication Auth { get; set; }

    public PokemonApiCaller(FactoryHttpClient factory)
    {
        _factory = factory;
        _client = _factory.Create();
    }

    public async Task<OutGetBasicInfoPokemon> GetBasicInfo(int pokemonId)
    {
        _client.SetAuthentication(Auth);
        HttpResponseMessage response = await _client.GetAsync($"api/v1/Pokemon/GetBasicInfo/{pokemonId}/").ConfigureAwait(false);
        if (response.IsSuccessStatusCode)
            return await response.Content.ReadAsAsync<OutGetBasicInfoPokemon>();

        return new OutGetBasicInfoPokemon();
    }

    public async Task<OutPokemon> GetInfo(int pokemonId, int versionId)
    {
        _client.SetAuthentication(Auth);
        HttpResponseMessage response = await _client.GetAsync($"api/v1/Pokemon/GetInfo/{pokemonId}/{versionId}").ConfigureAwait(false);
        if (response.IsSuccessStatusCode)
            return await response.Content.ReadAsAsync<OutPokemon>();

        return new OutPokemon();
    }

    public async Task Post(InPokemon pokemon)
    {
        _client.SetAuthentication(Auth);
        await _client.PostAsJsonAsync($"api/v1/Pokemon/Post", pokemon).ConfigureAwait(false);
    }

    public async Task Put(InPokemon pokemon)
    {
        _client.SetAuthentication(Auth);
        await _client.PutAsJsonAsync($"api/v1/Pokemon/Put", pokemon).ConfigureAwait(false);
    }

    public async Task Delete(int pokemonId)
    {
        _client.SetAuthentication(Auth);
        await _client.DeleteAsync($"api/v1/Pokemon/Delete/{pokemonId}").ConfigureAwait(false);
    }
        
    public async Task<OutGetBasicInfoPokemon> GetPagedBasicInfo(int page, int quantity)
    {
        _client.SetAuthentication(Auth);
        HttpResponseMessage response = await _client.GetAsync($"api/v1/Pokemon/GetPagedBasicInfo/{page}/{quantity}").ConfigureAwait(false);
        if (response.IsSuccessStatusCode)
            return await response.Content.ReadAsAsync<OutGetBasicInfoPokemon>();

        return new OutGetBasicInfoPokemon();
    }

    public async Task<long> GetNumberOfPokemons()
    {
        _client.SetAuthentication(Auth);
        HttpResponseMessage response = await _client.GetAsync($"api/v1/Pokemon/GetNumberOfPokemons").ConfigureAwait(false);
        if (response.IsSuccessStatusCode)
            return await response.Content.ReadAsAsync<long>();

        return 0;
    }
}