using PokedexServices.Interfaces;
using PokedexServices.Model;
using SqlServerADOConnection.SQLConnection;
using System;
using System.Data;

namespace PokedexServices.Services.ADO;

public class PokedexADOSqlServer : IPokedexService
{
    protected readonly ISqlServerADOQuery _sql;

    public PokedexADOSqlServer(ISqlServerADOQuery sql)
    {
        _sql = sql;
    }

    public IEnumerable<PokemonModel> GetPokemon()
    {
        DataSet ds = _sql.ExecuteQueryStoredProcedure("sp_pokedex_GetAllPokemon", new { });

        if (ds == null || ds.Tables[0].Rows.Count == 0)
            return Enumerable.Empty<PokemonModel>();

        var ls = new List<PokemonModel>();
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            ls.Add(ReadDataSet(ds.Tables[0].Rows[i]));

        return ls;
    }

    public PokemonModel GetPokemon(int pokemonId)
    {
        DataSet ds = _sql.ExecuteQueryStoredProcedure("sp_pokedex_GetPokemon", new { pokemonId, versionId = DBNull.Value });

        return ReadDataSet(ds.Tables[0].Rows[0]);
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

    protected PokemonModel ReadDataSet(DataRow ds)
    {
        return new PokemonModel
        {
            Id = (int)ds["id"],
            Identifier = ds["identifier"]?.ToString(),
            Height = (int)ds["Height"],
            Weight = (double)ds["Weight"]
        };
    }
}
