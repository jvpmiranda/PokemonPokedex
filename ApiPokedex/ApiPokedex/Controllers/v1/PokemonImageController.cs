using ApiPokedex.Contract.v1.Out;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        var pokemon = _image.GetImage(pokemonId);
        var result = _mapper.Map<OutImage>(pokemon);
        return Ok(result);
    }
}