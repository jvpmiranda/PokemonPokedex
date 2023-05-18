using ApiPokedex.Contract.v1;
using ApiPokedex.Interfaces;
using ApiPokedex.Model;
using ApiPokedex.Route;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiPokedex.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class PokedexController : ControllerBase
    {
        private IPokedexService _pokedex { get; }

        public PokedexController(IPokedexService pokedex)
        {
            _pokedex = pokedex;
        }

        [HttpGet(Routes.Get)]
        [MapToApiVersion("1.0")]
        public IEnumerable<Pokemon> GetAllv1()
        {
            return _pokedex.GetPokemon().Select(e => ConvertToContract(e));
        }

        [HttpGet(Routes.GetId)]
        [MapToApiVersion("1.0")]
        public Pokemon Get(string pokemonId)
        {
            return ConvertToContract(_pokedex.GetPokemon(Convert.ToInt32(pokemonId)));
        }

        [Authorize(Roles = "user")]
        [HttpPost(Routes.Post)]
        [MapToApiVersion("1.0")]
        public ActionResult Post(Pokemon pokemon)
        {
            _pokedex.Insert(ConvertToModel(pokemon));
            return Ok();
        }

        [Authorize(Roles = "user, admin")]
        [HttpPut(Routes.Put)]
        [MapToApiVersion("1.0")]
        public ActionResult Put(Pokemon pokemon)
        {
            _pokedex.Update(ConvertToModel(pokemon));
            return Ok();
        }

        [Authorize(Roles = "admin")]
        [HttpDelete(Routes.Delete)]
        [MapToApiVersion("1.0")]
        public ActionResult Delete(int pokemonId)
        {
            _pokedex.Delete(pokemonId);
            return Ok();
        }

        private PokemonPokedex ConvertToModel(Pokemon pokemon)
        {
            PokemonPokedex pok = new PokemonPokedex()
            {
                Id = pokemon.PokemonId,
                Name = pokemon.PokemonName
            };
            return pok;
        }

        private Pokemon ConvertToContract(PokemonPokedex pokemon)
        {
            Pokemon pok = new Pokemon()
            {
                PokemonId = pokemon.Id,
                PokemonName = pokemon.Name
            };
            return pok;
        }
    }
}