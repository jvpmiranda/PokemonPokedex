using ApiPokedex.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;

namespace ApiPokedex.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]")]
    //[RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
    public class Generation1Controller : ControllerBase
    {
        [HttpGet]
        [Route("GetAll")]
        public IEnumerable<Pokemon> GetAll()
        {
            throw new Exception("Erro no metodo GET");
        }

        [HttpGet]
        [Route("Get")]
        public Pokemon Get([FromQuery]int pokemonId)
        {
            throw new Exception($"Erro no metodo GET com o parametro {pokemonId}");
        }

        [HttpPost]
        public ActionResult Post(Pokemon pokemon)
        {
            return Ok();
        }

        [HttpPut]
        public ActionResult Put(Pokemon pokemon)
        {
            return Ok();
        }

        [HttpDelete]
        public ActionResult Delete(int pokemonId)
        {
            return Ok();
        }
    }
}