using DapperConnection.DataAccess;
using PokedexModels.Model;
using PokedexDataAccess.Interfaces;
using PokedexEF.Model;
using System;

namespace PokedexDataAccess.DataAccess.Dapper;

public class PokedexDapper : IPokedexDataAccessService
{
    protected readonly ISqlDapperDataAccess _sql;

    public PokedexDapper(ISqlDapperDataAccess sql)
    {
        _sql = sql;
    }

    public IEnumerable<PokemonModel> GetPokemon(string pokemonKey)
    {
        //var pokemon = _sql.ExecuteQueryStoredProcedure<PokemonModel, PokemonModel, PokemonModel, dynamic>(
        //    "sp_pokedex_GetBasicInfoPokemon",
        //    (pok, evol) =>
        //    {
        //        pok.EvolvesFrom = evol;
        //        return pok;
        //    },
        //    new { pokemonKey }, splitOn: "evolvesFromId"
        //).Result;
        //
        //return pokemon;
        return _sql.ExecuteQueryStoredProcedure<PokemonModel, dynamic>("sp_pokedex_GetBasicInfoPokemon", new { pokemonKey }).Result;
    }

    public PokemonModel GetPokemon(int pokemonId, int versionId)
    {
        var pokemon = _sql.ExecuteQueryStoredProcedure<PokemonModel, TypeModel, int?, PokemonModel, dynamic>(
            "sp_pokedex_GetPokemon",
            (pok, type, evol) =>
            {
                pok.Types.Add(type);
                if (evol.HasValue)
                    pok.EvolvesTo.Add(evol.Value);
                return pok;
            },
            new { pokemonId, versionId }, splitOn: "typeId, evolvesTo"
        ).Result;

        if (pokemon.Count() == 0)
            return new PokemonModel();

        var result = pokemon.GroupBy(p => p.Id).Select(g =>
        {
            var pok = g.First();
            pok.Types = g.Select(p => p.Types.Single()).ToList();
            if (pok.EvolvesTo.Count > 0)
                pok.EvolvesTo = g.Select(p => p.EvolvesTo.Single()).ToList();
            return pok;
        });
        var res = result.FirstOrDefault();
        res.Types = res.Types.GroupBy(t => t.Id).Select(t => t.First()).ToList();
        res.EvolvesTo = res.EvolvesTo.GroupBy(e => e).Select(t => t.First()).ToList();
        return res;
    }

    public void Insert(PokemonModel pokemon)
    {
        _sql.ExecuteNonQueryStoredProcedure("sp_pokedex_InsertPokemon", pokemon);
    }

    public void Update(PokemonModel pokemon)
    {
        _sql.ExecuteNonQueryStoredProcedure("sp_pokedex_UpdatePokemon", pokemon);
    }

    public void Delete(int pokemonId)
    {
        _sql.ExecuteNonQueryStoredProcedure("sp_pokedex_DeletePokemon", new { Id = pokemonId });
    }
}
