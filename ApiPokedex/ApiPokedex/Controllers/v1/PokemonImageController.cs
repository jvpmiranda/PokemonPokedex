using ApiPokedex.Contract.v1.In;
using ApiPokedex.Contract.v1.Out;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PokedexModels.Model;
using PokedexServices.Interfaces;
using System.Data;

namespace ApiPokedex.Controllers.v1;

[ApiController]
[Route("api/v{version:apiVersion}/[Controller]/[Action]")]
[ApiVersion("1.0")]
public class PokemonImageController : ControllerBase
{
    private readonly IImageService _image;
    private readonly IMapper _mapper;

    public PokemonImageController(IImageService image, IMapper mapper)
    {
        _image = image;
        _mapper = mapper;
    }

    /// <summary>
    /// Get pokemon's icon
    /// </summary>
    /// <param name="pokemonKey">Idto be used to search the pokemon</param>
    /// <returns>Array of bytes representing the image of the pokemon</returns>
    [Authorize(Roles = "Authenticated")]
    [HttpGet("{pokemonId}")]
    public ActionResult<OutImage> GetImage(int pokemonId)
    {
        var pokemon = _image.GetImage(pokemonId).Result;
        var result = _mapper.Map<OutImage>(pokemon);
        return Ok(result);
    }

    [Authorize(Roles = "Authenticated")]
    [HttpPost]
    public ActionResult Post(InImage image)
    {
        var pokedex = _mapper.Map<ImageModel>(image);
        _image.Insert(pokedex);
        return Ok();
    }

    [Authorize(Roles = "Authenticated")]
    [HttpPut]
    public ActionResult Put(InImage image)
    {
        _image.Update(_mapper.Map<ImageModel>(image));
        return Ok();
    }

    [Authorize(Roles = "Authenticated")]
    [HttpDelete("{versionId}")]
    public ActionResult Delete(int pokemonId)
    {
        _image.Delete(pokemonId);
        return Ok();
    }
}