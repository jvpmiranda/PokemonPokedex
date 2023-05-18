using ApiPokedex.Contract;
using ApiPokedex.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;

namespace ApiPokedex.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PokedexController : ControllerBase
    {
        private IPokedexService _pokedex { get; }

        public PokedexController(IPokedexService pokedex)
        {
            _pokedex = pokedex;
        }

        [HttpGet("GetAll")]
        public IEnumerable<Pokemon> GetAll()
        {
            return _pokedex.GetPokemon();
        }

        [HttpGet("Get")]
        public Pokemon Get([FromQuery]int pokemonId)
        {
            return _pokedex.GetPokemon(pokemonId);
        }

        [Authorize]
        [HttpPost("Post")]
        public ActionResult Post(Pokemon pokemon)
        {
            _pokedex.Insert(pokemon);
            return Ok();
        }

        [Authorize(Roles = "user")]
        [HttpPut("Put")]
        public ActionResult Put(Pokemon pokemon)
        {
            _pokedex.Update(pokemon);
            return Ok();
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("Delete")]
        public ActionResult Delete(int pokemonId)
        {
            _pokedex.Delete(pokemonId);
            return Ok();
        }
    }
}