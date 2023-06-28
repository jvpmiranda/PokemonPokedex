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
    /// <param name="pokemonId">Id to be used to search the pokemon If null returns all Pokemon</param>
    /// <returns>List of N pokemon that match the key used for searching</returns>
    [Authorize(Roles = "Authenticated")]
    [HttpGet("{pokemonId?}")]
    public ActionResult<OutGetBasicInfoPokemon> GetBasicInfo(int? pokemonId = null)
    {
        if (pokemonId.HasValue && pokemonId.Value == 10)
        throw new Exception("teste");
        var pokemon = _pokedex.GetBasicPokemon(pokemonId).Result;
        var result = _mapper.Map<IEnumerable<OutBasicInfoPokemon>>(pokemon);
        return Ok(result);
    }

    [Authorize(Roles = "Authenticated")]
    [HttpGet("{pokemonId}")]
    public ActionResult<OutPokemon> GetInfo(int pokemonId)
    {
        var pok = _pokedex.GetPokemon(pokemonId).Result;
        var result = _mapper.Map<OutPokemon>(pok);
        return Ok(result);
    }

    [Authorize(Roles = "Authenticated")]
    [HttpGet("{pokemonId}/{versionId}")]
    public ActionResult<OutFullPokemon> GetFullInfo(int pokemonId, int versionId)
    {
        var pok = _pokedex.GetPokemonFullInfo(pokemonId, versionId).Result;
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
    [HttpDelete("{pokemonId}")]
    public ActionResult Delete(int pokemonId)
    {
        _pokedex.Delete(pokemonId);
        return Ok();
    }
}