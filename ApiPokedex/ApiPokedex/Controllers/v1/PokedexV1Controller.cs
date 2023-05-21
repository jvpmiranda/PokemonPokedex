using ApiPokedex.Contract;
using ApiPokedex.Route;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PokedexServices.Interfaces;
using PokedexServices.Model;

namespace ApiPokedex.Controllers.v1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/Pokedex")]
    [ApiVersion("1.0")]
    public class PokedexV1Controller : ControllerBase
    {
        private IPokedexService _pokedex { get; }

        public PokedexV1Controller(IPokedexService pokedex)
        {
            _pokedex = pokedex;
        }

        [HttpGet(Routes.Get)]
        [MapToApiVersion("1.0")]
        public ActionResult<IEnumerable<PokemonV1>> GetAllv1()
        {
            return Ok(_pokedex.GetPokemon().Select(e => ConvertToContractV1(e)));
        }

        [HttpGet(Routes.GetId)]
        [MapToApiVersion("1.0")]
        public ActionResult<PokemonV1> Get(string pokemonId)
        {
            return Ok(ConvertToContractV1(_pokedex.GetPokemon(Convert.ToInt32(pokemonId))));
        }

        [Authorize(Roles = "user")]
        [HttpPost(Routes.Post)]
        [MapToApiVersion("1.0")]
        public ActionResult Post(PokemonV1 pokemon)
        {
            _pokedex.Insert(ConvertToModel(pokemon));
            return Ok();
        }

        [Authorize(Roles = "user, admin")]
        [HttpPut(Routes.Put)]
        [MapToApiVersion("1.0")]
        public ActionResult Put(PokemonV1 pokemon)
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

        private PokemonModel ConvertToModel(PokemonV1 pokemon)
        {
            PokemonModel pok = new PokemonModel()
            {
                Id = pokemon.PokemonId,
                Identifier = pokemon.PokemonName
            };
            return pok;
        }

        private PokemonV1 ConvertToContractV1(PokemonModel pokemon)
        {
            PokemonV1 pok = new PokemonV1()
            {
                PokemonId = pokemon.Id,
                PokemonName = pokemon.Identifier
            };
            return pok;
        }
    }
}