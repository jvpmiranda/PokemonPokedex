using ApiPokedex.Contract.v1.In;
using ApiPokedex.Contract.v1.Out;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PokedexModels.Model;
using PokedexServices.Interfaces;

namespace ApiPokedex.Controllers.v1;

[ApiController]
[Route("api/v{version:apiVersion}/[Controller]/[Action]")]
[ApiVersion("1.0")]
public class PokemonController : ControllerBase
{
    private readonly IPokemonService _pokedex;
    private readonly IMapper _mapper;

    public PokemonController(IPokemonService pokedex, IMapper mapper)
    {
        _pokedex = pokedex;
        _mapper = mapper;
    }

    /// <summary>
    /// Get basic info about the pokemon from the database
    /// </summary>
    /// <param name="pokemonId">Id to be used to search the pokemon If null returns all Pokemon</param>
    /// <returns>List of N pokemon that match the key used for searching</returns>
    [Authorize(Roles = "Authenticated")]
    [HttpGet("{pokemonId?}")]
    public async Task<ActionResult<OutGetBasicInfoPokemon>> GetBasicInfo(int? pokemonId = null)
    {
        var pokemon = await _pokedex.GetBasicPokemon(pokemonId);
        var result = _mapper.Map<IEnumerable<OutBasicInfoPokemon>>(pokemon);
        return Ok(result);
    }

    [Authorize(Roles = "Authenticated")]
    [HttpGet("{page}/{quantity}")]
    public async Task<ActionResult<OutGetBasicInfoPokemon>> GetPagedBasicInfo(int page, int quantity)
    {
        var pokemon = await _pokedex.GetPagedBasicPokemon(page, quantity);
        var result = _mapper.Map<IEnumerable<OutBasicInfoPokemon>>(pokemon);
        return Ok(result);
    }

    [Authorize(Roles = "Authenticated")]
    [HttpGet]
    public async Task<ActionResult<long>> GetNumberOfPokemons()
    {
        var result = await _pokedex.GetNumberOfPokemons();
        return Ok(result);
    }

    [Authorize(Roles = "Authenticated")]
    [HttpGet("{pokemonId}/{versionId}")]
    public async Task<ActionResult<OutPokemon>> GetInfo(int pokemonId, int versionId)
    {
        var pok = await _pokedex.GetPokemon(pokemonId, versionId);
        OutPokemon results = _mapper.Map<OutPokemon>(pok);
        return Ok(results);
    }

    [Authorize(Roles = "Authenticated")]
    [HttpPost]
    public ActionResult Post(InPokemon pokemon)
    {
        _pokedex.Insert(_mapper.Map<PokemonModel>(pokemon));
        return Ok();
    }

    [Authorize(Roles = "Authenticated")]
    [HttpPut]
    public ActionResult Put(InPokemon pokemon)
    {
        _pokedex.Update(_mapper.Map<PokemonModel>(pokemon));
        return Ok();
    }

    [Authorize(Roles = "Authenticated")]
    [HttpDelete("{pokemonId}")]
    public ActionResult Delete(int pokemonId)
    {
        _pokedex.Delete(pokemonId);
        return Ok();
    }
}