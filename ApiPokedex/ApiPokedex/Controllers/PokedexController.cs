using ApiPokedex.Interfaces;
using ApiPokedex.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;

namespace ApiPokedex.Controllers
{
    [ApiController]
    [Route("[controller]")]
    //[RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
    public class PokedexController : ControllerBase
    {
        private IPokedexService _pokedex { get; }

        public PokedexController(IPokedexService pokedex)
        {
            _pokedex = pokedex;
        }

        [HttpGet]
        [Route("GetAll")]
        public IEnumerable<Pokemon> GetAll()
        {
            return _pokedex.GetPokemon();
        }

        [HttpGet]
        [Route("Get")]
        public Pokemon Get([FromQuery]int pokemonId)
        {
            return _pokedex.GetPokemon(pokemonId);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Post(Pokemon pokemon)
        {
            _pokedex.Insert(pokemon);
            return Ok();
        }

        [Authorize]
        [HttpPut]
        public ActionResult Put(Pokemon pokemon)
        {
            _pokedex.Update(pokemon);
            return Ok();
        }

        [Authorize]
        [HttpDelete]
        public ActionResult Delete(int pokemonId)
        {
            _pokedex.Delete(pokemonId);
            return Ok();
        }
    }
}