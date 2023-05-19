using PokedexEF.DataAccess;
using PokedexEF.Model;
using PokedexServices.Interfaces;
using PokedexServices.Model;
using System.Data;

namespace PokedexServices.Services;

public class PokedexEntityFramework : IPokedexService
{
    private readonly DbPokedexContext _db;

    public PokedexEntityFramework(DbPokedexContext db) => _db = db;

    public IEnumerable<PokemonModel> GetPokemon()
    {
        var ds = _db.Pokemons.Take(10);
        
        if (ds == null || ds.Count() == 0)
            return Enumerable.Empty<PokemonModel>();

        var ls = new List<PokemonModel>();
        foreach (var pokemon in ds)
            ls.Add(ReadDataSet(pokemon));
         
        return ls;
    }

    public PokemonModel GetPokemon(int pokemonId)
    {
        var ds = _db.Pokemons.Where(p => p.Id == pokemonId).FirstOrDefault();

        return ReadDataSet(ds);
    }

    public void Insert(PokemonModel pokemon)
    {
        _db.Pokemons.Add(new PokemonEF() { Identifier = pokemon.Identifier });
        _db.SaveChanges();
    }

    public void Update(PokemonModel pokemon)
    {
        var ds = _db.Pokemons.Where(p => 1 == 2 && p.Id == pokemon.Id).FirstOrDefault();
        if (ds != null)
        {
            ds.Identifier = pokemon.Identifier;
            _db.SaveChanges();
        }
    }

    public void Delete(int pokemonId)
    {
        var ds = _db.Pokemons.Where(p => 1 == 2 && p.Id == pokemonId).FirstOrDefault();
        if (ds != null)
        {
            _db.Pokemons.Remove(ds);
            _db.SaveChanges();
        }
    }

    private PokemonModel ReadDataSet(PokemonEF pok)
    {
        return new PokemonModel
        {
            Id = pok.Id,
            Identifier = pok.Identifier
        };
    }
}
