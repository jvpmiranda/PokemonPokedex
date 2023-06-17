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
    private readonly IPokedexService _pokedex;
    private readonly IMapper _mapper;

    public PokemonController(IPokedexService pokedex, IMapper mapper)
    {
        _pokedex = pokedex;
        _mapper = mapper;
    }

    /// <summary>
    /// Get basic info about the pokemon from the database
    /// </summary>
    /// <param name="pokemonKey">Key to be used to search the pokemon. Can be the pokemon ID or part of name</param>
    /// <returns>List of N pokemon that match the key used for searching</returns>
    [HttpGet]
    public ActionResult<OutGetBasicInfoPokemon> GetBasicInfo(string? pokemonKey)
    {
        var pokemon = _pokedex.GetPokemon(pokemonKey);
        var result = _mapper.Map<IEnumerable<OutBasicInfoPokemon>>(pokemon);
        return Ok(result);
    }

    [HttpGet]
    public ActionResult<OutPokemon> GetInfo(int pokemonId, int versionId)
    {
        var pok = _pokedex.GetPokemon(pokemonId, versionId);
        var result = _mapper.Map<OutPokemon>(pok);
        return Ok(result);
    }

    [HttpGet]
    public ActionResult<OutFullPokemon> GetFullInfo(int pokemonId, int versionId)
    {
        var pok = _pokedex.GetPokemonFullInfo(pokemonId, versionId);
        OutFullPokemon results = _mapper.Map<OutFullPokemon>(pok);
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
    [HttpDelete]
    [MapToApiVersion("1.0")]
    public ActionResult Delete(int pokemonId)
    {
        _pokedex.Delete(pokemonId);
        return Ok();
    }
}