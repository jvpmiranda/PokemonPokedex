using Dapper;
using PokedexDataAccess.DataAccess.Dapper.Abstract;
using PokedexDataAccess.Extensions.Dapper;
using PokedexDataAccess.Factory;
using PokedexDataAccess.Interfaces;
using PokedexDataAccess.Interfaces.Infrastructure;
using PokedexModels.Model;
using System.Data;

namespace PokedexDataAccess.DataAccess.Dapper;

public class PokemonTypesDapper : ConnectionDapper, IPokemonTypesDataAccess
{
    public PokemonTypesDapper(FactoryDbConnection factory) : base(factory) { }

    public async Task Delete(int typeId)
    {
        using IDbConnection conn = GetDbConnection();
        await conn.ExecuteAsync("sp_pokedex_DeleteTypesOfPokemon", new { typeId }, commandType: CommandType.StoredProcedure);
    }

    public async Task DeleteInTransaction(int typeId, IDataAccessTransaction tran)
    {
        using IDbConnection conn = GetDbConnection();
        await conn.ExecuteAsync("sp_pokedex_DeleteTypesOfPokemon", new { typeId }, commandType: CommandType.StoredProcedure, transaction: GetDbTransaction(tran));
    }

    public async Task<IEnumerable<TypeModel>> Get(int? typeId)
    {
        using IDbConnection conn = GetDbConnection();
        return await conn.QueryAsync<TypeModel>("sp_pokedex_GetTypesOfPokemon", new { typeId }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<TypeModel>> GetInTransaction(int? typeId, IDataAccessTransaction tran)
    {
        using IDbConnection conn = GetDbConnection();
        return await conn.QueryAsync<TypeModel>("sp_pokedex_GetTypesOfPokemon", new { typeId }, commandType: CommandType.StoredProcedure, transaction: GetDbTransaction(tran));
    }

    public async Task Upsert(IEnumerable<TypeModel> types)
    {
        using IDbConnection conn = GetDbConnection();
        var param = types.AsTableValuedParameter("tpTypeOfPokemon");

        await conn.ExecuteAsync("sp_pokedex_UpsertManyTypesOfPokemon", param, commandType: CommandType.StoredProcedure);
    }

    public async Task UpsertInTransaction(IEnumerable<TypeModel> type, IDataAccessTransaction tran)
    {
        using IDbConnection conn = GetDbConnection();
        var param = type.AsTableValuedParameter("tpTypeOfPokemon");

        await conn.ExecuteAsync("sp_pokedex_UpsertManyTypesOfPokemon", param, commandType: CommandType.StoredProcedure, transaction: GetDbTransaction(tran));
    }

    public async Task Upsert(TypeModel type)
    {
        using IDbConnection conn = GetDbConnection();
        await conn.ExecuteAsync("sp_pokedex_UpsertTypesOfPokemon", new { type.Id }, commandType: CommandType.StoredProcedure);
    }

    public async Task UpsertInTransaction(TypeModel type, IDataAccessTransaction tran)
    {
        using IDbConnection conn = GetDbConnection();
        await conn.ExecuteAsync("sp_pokedex_UpsertTypesOfPokemon", new { type.Id }, commandType: CommandType.StoredProcedure, transaction: GetDbTransaction(tran));
    }
}
