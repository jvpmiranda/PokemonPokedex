﻿using DapperConnection.DataAccess;
using PokedexModels.Model;
using PokedexServices.Interfaces;

namespace PokedexServices.Services.Dapper;

public class PokedexDapper : IPokedexService
{
    protected readonly ISqlDapperDataAccess _sql;

    public PokedexDapper(ISqlDapperDataAccess sql)
    {
        _sql = sql;
    }

    public IEnumerable<PokemonModel> GetPokemon(string pokemonKey)
    {
        return _sql.ExecuteQueryStoredProcedure<PokemonModel, dynamic>("sp_pokedex_GetBasicInfoPokemon", new { pokemonKey }).Result;
    }

    public PokemonModel GetPokemon(int pokemonId, int generationId)
    {
        var pokemon = _sql.ExecuteQueryStoredProcedure<PokemonModel, TypeModel, int, PokemonModel, dynamic>(
            "sp_pokedex_GetPokemon",
            (pok, type, evol) => 
            {
                pok.Types.Add(type);
                pok.EvolvesTo.Add(evol);
                return pok; 
            },
            new { pokemonId, generationId }, splitOn: "typeId, EvolvesTo"
        ).Result;

        if (pokemon.Count() == 0)
            return new PokemonModel();

        var result = pokemon.GroupBy(p => p.Id).Select(g =>
        {
            var groupedPost = g.First();
            groupedPost.Types = g.Select(p => p.Types.Single()).ToList();
            groupedPost.EvolvesTo = g.Select(p => p.EvolvesTo.Single()).ToList();
            return groupedPost;
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
