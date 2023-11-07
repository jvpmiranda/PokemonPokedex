using Dapper;
using PokedexDataAccess.DataAccess.Dapper.Abstract;
using PokedexDataAccess.Factory;
using PokedexDataAccess.Interfaces;
using PokedexDataAccess.Interfaces.Infrastructure;
using PokedexModels.Model;
using System.Data;

namespace PokedexDataAccess.DataAccess.Dapper;

public class PokedexVersionGroupDapper : ConnectionDapper, IPokedexVersionGroupDataAccess
{
    public PokedexVersionGroupDapper(FactoryDbConnection factory) : base(factory) { }

    public async Task Delete(int versionGroupId)
    {
        using IDbConnection conn = GetDbConnection();
        await conn.ExecuteAsync("sp_pokedex_DeletePokedexVersionGroup", new { versionGroupId }, commandType: CommandType.StoredProcedure);
    }

    public void DeleteInTransaction(int versionGroupId, IDataAccessTransaction tran)
    {
        var conn = GetDbConnection(tran);
        conn.Execute("sp_pokedex_DeletePokedexVersionGroup", new { versionGroupId }, commandType: CommandType.StoredProcedure, transaction: GetDbTransaction(tran));
    }

    public async Task<IEnumerable<PokedexVersionGroupModel>> Get(int? versionGroupId)
    {
        using IDbConnection conn = GetDbConnection();
        return await conn.QueryAsync<PokedexVersionGroupModel>("sp_pokedex_GetPokedexVersionsGroup", new { versionGroupId }, commandType: CommandType.StoredProcedure);
    }

    public IEnumerable<PokedexVersionGroupModel> GetInTransaction(int? versionGroupId, IDataAccessTransaction tran)
    {
        var conn = GetDbConnection(tran);
        return conn.Query<PokedexVersionGroupModel>("sp_pokedex_GetPokedexVersionsGroup", new { versionGroupId }, commandType: CommandType.StoredProcedure, transaction: GetDbTransaction(tran));
    }

    public async Task Upsert(PokedexVersionGroupModel version)
    {
        using IDbConnection conn = GetDbConnection();
        await conn.ExecuteAsync("sp_pokedex_UpsertPokedexVersionGroups", version, commandType: CommandType.StoredProcedure);
    }

    public void UpsertInTransaction(PokedexVersionGroupModel version, IDataAccessTransaction tran)
    {
        var conn = GetDbConnection(tran);
        conn.Execute("sp_pokedex_UpsertPokedexVersionGroups", version, commandType: CommandType.StoredProcedure, transaction: GetDbTransaction(tran));
    }
}