using PokedexEF.DataAccess;
using PokedexServices.Interfaces;
using PokedexServices.Model;
using System.Data;

namespace PokedexServices.Services.EF;

public class PokedexEntityFrameworkV2 : PokedexEntityFramework, IPokedexServiceV2
{
    public PokedexEntityFrameworkV2(DbPokedexContext db) : base(db) { }
    
    public PokemonModel GetPokemon(int pokemonId, int versionId)
    {
        var ds = _db.Pokemons.Where(p => p.Id == pokemonId).First();

        return ReadDataSet(ds);
    }

}
