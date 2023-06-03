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

    public IEnumerable<PokemonModel> GetPokemon()
    {
        //return _sql.ExecuteQuery<PokemonModel, dynamic>("select top 10 * from pokemon", new { }).Result;
        return _sql.ExecuteQueryStoredProcedure<PokemonModel, dynamic>("sp_pokedex_GetAllPokemon", new { }).Result;
    }
    public PokemonModel GetPokemon(int pokemonId)
    {
        PokemonModel pokemon;
        using (var queryResult = _sql.ExecuteQueryStoredProcedureMultiple<dynamic>("sp_pokedex_GetPokemon", new { pokemonId, versionId = 0 }).Result)
        {
            pokemon = queryResult.Read<PokemonModel>().ToList().First();
            pokemon.Types = queryResult.Read<PokemonTypeModel>().ToList();
        }

        return pokemon;
    }

    public void Insert(PokemonModel pokemon)
    {
        //_sql.ExecuteNonQuery("insert into pokemon (identifier) values (@Identifier)", pokemon);
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
