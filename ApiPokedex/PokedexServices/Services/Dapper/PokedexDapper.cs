using DapperConnection.DataAccess;
using PokedexServices.Interfaces;
using PokedexServices.Model;
using System;

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
        IEnumerable<PokemonModel> pokemon;
        using (var queryResult = _sql.ExecuteQueryStoredProcedureMultiple<dynamic>("sp_pokedex_GetPokemon", new { pokemonKey }).Result)
        {
            pokemon = queryResult.Read<PokemonModel>().ToList();
        }

        return pokemon;
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
