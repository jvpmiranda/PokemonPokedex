using ApiPokedex.Contract.In;
using ApiPokedex.Contract.Out;
using ApiPokedex.Mapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PokedexServices.Interfaces;

namespace ApiPokedex.Controllers.v1;

[ApiController]
[Route("api/v{version:apiVersion}/[Controller]/[Action]")]
[ApiVersion("1.0")]
public class PokemonController : ControllerBase
{
    private IPokedexService _pokedex { get; }

    public PokemonController(IPokedexService pokedex)
    {
        _pokedex = pokedex;
    }

    /// <summary>
    /// Get basic info about the pokemon from the database
    /// </summary>
    /// <param name="pokemonKey">Key to be used to search the pokemon. Can be the pokemon ID or part of name</param>
    /// <returns>List of N pokemon that match the key used for searching</returns>
    [HttpGet]
    public ActionResult<OutGetBasicInfoPokemon> GetBasicInfo(string? pokemonKey)
    {
        OutGetBasicInfoPokemon result = new OutGetBasicInfoPokemon();
        result.Pokemons = MapperPokedex.ConvertToEnumerableOutBasicInfoPokemon(_pokedex.GetPokemon(pokemonKey));
        return Ok(result);
    }

    [HttpGet]
    public ActionResult<OutPokemon> GetInfo(int pokemonId, int versionId)
    {
        var pok = _pokedex.GetPokemon(pokemonId, versionId);
        OutPokemon result = MapperPokedex.ConvertToOutPokemon(pok);
        return Ok(result);
    }

    [HttpGet]
    public ActionResult<OutFullPokemon> GetFullInfo(int pokemonId, int versionId)
    {
        var pok = _pokedex.GetPokemonFullInfo(pokemonId, versionId);
        OutFullPokemon result = MapperPokedex.ConvertToOutFullPokemon(pok);
        return Ok(result);
    }

    [Authorize(Roles = "admin")]
    [HttpPost]
    public ActionResult Post(InPokemon pokemon)
    {
        _pokedex.Insert(MapperPokedex.ConvertToModel(pokemon));
        return Ok();
    }

    [Authorize(Roles = "user, admin")]
    [HttpPut]
    public ActionResult Put(InPokemon pokemon)
    {
        _pokedex.Update(MapperPokedex.ConvertToModel(pokemon));
        return Ok();
    }

    [Authorize(Roles = "admin")]
    [HttpDelete]
    [MapToApiVersion("1.0")]
    public ActionResult Delete(int pokemonId)
    {
        _pokedex.Delete(pokemonId);
        return Ok();
    }
}