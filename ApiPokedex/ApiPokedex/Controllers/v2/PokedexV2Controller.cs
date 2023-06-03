using ApiPokedex.Contract;
using ApiPokedex.Route;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PokedexServices.Interfaces;
using PokedexServices.Model;

namespace ApiPokedex.Controllers.v2
{
    [ApiController]
    [Route("api/v{version:apiVersion}/Pokedex")]
    [ApiVersion("2.0")]
    public class PokedexV2Controller : ControllerBase
    {
        private IPokedexServiceV2 _pokedex { get; }

        public PokedexV2Controller(IPokedexServiceV2 pokedex)
        {
            _pokedex = pokedex;
        }

        [HttpGet(Routes.Get)]
        [MapToApiVersion("2.0")]
        public ActionResult<IEnumerable<PokemonV2>> GetAllv2()
        {
            return Ok(_pokedex.GetPokemon().Select(e => ConvertToContractV2(e)));
        }

        [HttpGet(Routes.GetId)]
        [MapToApiVersion("2.0")]
        public ActionResult<PokemonV1> Get(int pokemonId, int versionId)
        {
            return Ok(ConvertToContractV2(_pokedex.GetPokemon(pokemonId, versionId)));
        }

        //[Authorize(Roles = "user")]
        //[HttpPost(Routes.Post)]
        //[MapToApiVersion("2.0")]
        //public ActionResult Post(PokemonV1 pokemon)
        //{
        //    _pokedex.Insert(ConvertToModel(pokemon));
        //    return Ok();
        //}

        //[Authorize(Roles = "user, admin")]
        //[HttpPut(Routes.Put)]
        //[MapToApiVersion("1.0")]
        //public ActionResult Put(PokemonV1 pokemon)
        //{
        //    _pokedex.Update(ConvertToModel(pokemon));
        //    return Ok();
        //}

        [Authorize(Roles = "admin")]
        [HttpDelete(Routes.Delete)]
        [MapToApiVersion("2.0")]
        public ActionResult Delete(int pokemonId)
        {
            _pokedex.Delete(pokemonId);
            return Ok();
        }


        private PokemonModel ConvertToModel(PokemonV2 pokemon)
        {
            PokemonModel pok = new PokemonModel()
            {
                Id = pokemon.PokemonId,
                Identifier = pokemon.PokemonName,
                Height = pokemon.Height,
                Weight = pokemon.Weight
            };

            pok.Types = new List<PokemonTypeModel>();
            foreach (var type in pokemon.Types)
                pok.Types.Add(new PokemonTypeModel
                {
                    Id = type.TypeId,
                    Identifier = type.TypeName
                });

            return pok;
        }

        private PokemonV2 ConvertToContractV2(PokemonModel pokemon)
        {
            PokemonV2 pok = new PokemonV2()
            {
                PokemonId = pokemon.Id,
                PokemonName = pokemon.Identifier,
                Height = pokemon.Height,
                Weight = pokemon.Weight
            };

            pok.Types = new List<PokemonTypeV2>();
            foreach (var type in pokemon.Types)
                pok.Types.Add(new PokemonTypeV2
                {
                    TypeId = type.Id,
                    TypeName = type.Identifier
                });
            pok.Description = pokemon.Description.FirstOrDefault()?.Description;               

            return pok;
        }
    }
}