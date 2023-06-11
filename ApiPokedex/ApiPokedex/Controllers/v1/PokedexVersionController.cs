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
public class PokedexVersionController : ControllerBase
{
    private IPokedexVersionService _pokedexVersion { get; }

    public PokedexVersionController(IPokedexVersionService pokedexVersion)
    {
        _pokedexVersion = pokedexVersion;
    }

    [HttpGet]
    public ActionResult<OutGetPokedexVersion> GetVersion(int? versionId)
    {
        OutGetPokedexVersion result = new OutGetPokedexVersion();
        result.Versions = MapperPokedex.ConvertToEnumerableOutVersion(_pokedexVersion.Get(versionId));
        return Ok(result);
    }

    [Authorize(Roles = "admin")]
    [HttpPost]
    public ActionResult Post(InPokedexVersion pokedexVersion)
    {
        _pokedexVersion.Insert(MapperPokedex.ConvertToModel(pokedexVersion));
        return Ok();
    }

    [Authorize(Roles = "user, admin")]
    [HttpPut]
    public ActionResult Put(InPokedexVersion pokedexVersion)
    {
        _pokedexVersion.Update(MapperPokedex.ConvertToModel(pokedexVersion));
        return Ok();
    }

    [Authorize(Roles = "admin")]
    [HttpDelete]
    public ActionResult Delete(int pokemonId)
    {
        _pokedexVersion.Delete(pokemonId);
        return Ok();
    }
}