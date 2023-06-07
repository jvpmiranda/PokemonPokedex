using ApiPokedex.Contract.In;
using ApiPokedex.Contract.Out;
using ApiPokedex.Mapper;
using ApiPokedex.Route;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PokedexServices.Interfaces;

namespace ApiPokedex.Controllers.v1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/Pokedex")]
    [ApiVersion("1.0")]
    public class PokedexController : ControllerBase
    {
        private IPokedexService _pokedex { get; }

        public PokedexController(IPokedexService pokedex)
        {
            _pokedex = pokedex;
        }

        /// <summary>
        /// Get basic info about the pokemon from the database
        /// </summary>
        /// <param name="pokemonKey">Key to be used to search the pokemon. Can be the pokemon ID or part of name</param>
        /// <returns>List of N pokemon that match the key used for searching</returns>
        [HttpGet(Routes.GetBasicInfo)]
        [MapToApiVersion("1.0")]
        public ActionResult<OutGetBasicInfoPokemon> GetBasicInfo(string? pokemonKey)
        {
            OutGetBasicInfoPokemon result = new OutGetBasicInfoPokemon();
            result.Pokemons = MapperPokedex.ConvertToEnumerableContract(_pokedex.GetPokemon(pokemonKey));
            return Ok(result);
        }

        [Authorize(Roles = "admin")]
        [HttpPost(Routes.Post)]
        [MapToApiVersion("1.0")]
        public ActionResult Post(InPokemon pokemon)
        {
            _pokedex.Insert(MapperPokedex.ConvertToModel(pokemon));
            return Ok();
        }

        [Authorize(Roles = "user, admin")]
        [HttpPut(Routes.Put)]
        [MapToApiVersion("1.0")]
        public ActionResult Put(InPokemon pokemon)
        {
            _pokedex.Update(MapperPokedex.ConvertToModel(pokemon));
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
    }
}