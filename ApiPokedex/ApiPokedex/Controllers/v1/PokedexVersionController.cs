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
public class PokedexVersionController : ControllerBase
{
    private IPokedexVersionService _pokedexVersion { get; }
    private readonly IMapper _mapper;

    public PokedexVersionController(IPokedexVersionService pokedexVersion, IMapper mapper)
    {
        _pokedexVersion = pokedexVersion;
        _mapper = mapper;
    }

    [Authorize(Roles = "Authenticated")]
    [HttpGet("{versionId?}")]
    public ActionResult<OutGetPokedexVersion> GetVersion(int? versionId = null)
    {
        var pokedex = _pokedexVersion.Get(versionId).Result;
        OutGetPokedexVersion result = _mapper.Map<OutGetPokedexVersion>(pokedex);
        return Ok(result);
    }

    [Authorize(Roles = "Authenticated")]
    [HttpPost]
    public ActionResult Post(InPokedexVersion pokedexVersion)
    {
        var pokedex = _mapper.Map<PokedexVersionModel>(pokedexVersion);
        _pokedexVersion.Insert(pokedex);
        return Ok();
    }

    [Authorize(Roles = "Authenticated")]
    [HttpPut]
    public ActionResult Put(InPokedexVersion pokedexVersion)
    {
        _pokedexVersion.Update(_mapper.Map<PokedexVersionModel>(pokedexVersion));
        return Ok();
    }

    [Authorize(Roles = "Authenticated")]
    [HttpDelete("{versionId}")]
    public ActionResult Delete(int versionId)
    {
        _pokedexVersion.Delete(versionId);
        return Ok();
    }
}