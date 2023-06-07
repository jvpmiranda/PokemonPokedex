using Microsoft.EntityFrameworkCore;
using PokedexEF.DataAccess;
using PokedexEF.Model;
using PokedexServices.Interfaces;
using PokedexServices.Model;
using System.Data;

namespace PokedexServices.Services.EF;

public class PokedexEntityFramework : IPokedexService
{
    protected readonly DbPokedexContext _db;

    public PokedexEntityFramework(DbPokedexContext db) => _db = db;

    public IEnumerable<PokemonModel> GetPokemon(string pokemonKey)
    {
        int.TryParse(pokemonKey, out int id);
        var ds = _db.Pokemons.Where(p => p.Id == id || p.Identifier.Contains(pokemonKey));

        if (ds == null || ds.Count() == 0)
            return Enumerable.Empty<PokemonModel>();

        var ls = new List<PokemonModel>();
        foreach (var pokemon in ds)
            ls.Add(ReadDataSet(pokemon));

        return ls;
    }

    public void Insert(PokemonModel pokemon)
    {
        _db.Pokemons.Add(new PokemonEF() { Identifier = pokemon.Identifier });
        _db.SaveChanges();
    }

    public void Update(PokemonModel pokemon)
    {
        var ds = _db.Pokemons.Where(p => p.Id == pokemon.Id).FirstOrDefault();
        if (ds != null)
        {
            ds.Identifier = pokemon.Identifier;
            //_db.SaveChanges();
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

    protected PokemonModel ReadDataSet(PokemonEF pok)
    {
        var pokemon = new PokemonModel
        {
            Id = pok.Id,
            Identifier = pok.Identifier,
            Height = pok.Height,
            Weight = pok.Weight
        };

        pokemon.Types = new List<TypeModel>();
        foreach (var type in pok.PokemonTypes)
        {
            pokemon.Types.Add(new TypeModel
            {
                Id = type.Type.Id,
                Identifier = type.Type.Identifier
            });
        }

        return pokemon;
    }
}
