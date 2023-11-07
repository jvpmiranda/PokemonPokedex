using Dapper;
using PokedexDataAccess.DataAccess.Dapper.Abstract;
using PokedexDataAccess.Factory;
using PokedexDataAccess.Interfaces;
using PokedexDataAccess.Interfaces.Infrastructure;
using PokedexModels.Model;
using System.Data;

namespace PokedexDataAccess.DataAccess.Dapper;

public class PokedexVersionDapper : ConnectionDapper, IPokedexVersionDataAccess
{
    public PokedexVersionDapper(FactoryDbConnection factory) : base(factory) { }

    public async Task Delete(int versionId)
    {
        using IDbConnection conn = GetDbConnection();
        await conn.ExecuteAsync("sp_pokedex_DeletePokedexVersion", new { versionId }, commandType: CommandType.StoredProcedure);
    }

    public void DeleteInTransaction(int versionId, IDataAccessTransaction tran)
    {
        var conn = GetDbConnection(tran);
        conn.Execute("sp_pokedex_DeletePokedexVersion", new { versionId }, commandType: CommandType.StoredProcedure, transaction: GetDbTransaction(tran));
    }

    public async Task<IEnumerable<PokedexVersionModel>> Get(int? versionId)
    {
        using IDbConnection conn = GetDbConnection();
        return await conn.QueryAsync<PokedexVersionModel, PokedexVersionGroupModel, PokedexVersionModel>(
            "sp_pokedex_GetPokedexVersions",
            (vers, group) =>
            {
                vers.VersionGroup = group;
                return vers;
            },
            new { versionId }, 
            splitOn: "groupId",
            commandType: CommandType.StoredProcedure);
    }

    public IEnumerable<PokedexVersionModel> GetInTransaction(int? versionId, IDataAccessTransaction tran)
    {
        var conn = GetDbConnection(tran);
        return conn.Query<PokedexVersionModel, PokedexVersionGroupModel, PokedexVersionModel>(
            "sp_pokedex_GetPokedexVersions",
            (vers, group) =>
            {
                vers.VersionGroup = group;
                return vers;
            },
            new { versionId },
            splitOn: "groupId",
            commandType: CommandType.StoredProcedure, 
            transaction: GetDbTransaction(tran));
    }

    public async Task Upsert(PokedexVersionModel version)
    {
        using IDbConnection conn = GetDbConnection();
        await conn.ExecuteAsync("sp_pokedex_UpsertPokedexVersion", new { version.Id, version.Name, version.VersionGroup.GroupId }, commandType: CommandType.StoredProcedure);
    }

    public void UpsertInTransaction(PokedexVersionModel version, IDataAccessTransaction tran)
    {
        var conn = GetDbConnection(tran);
        conn.Execute("sp_pokedex_UpsertPokedexVersion", new { version.Id, version.Name, version.VersionGroup.GroupId }, commandType: CommandType.StoredProcedure, transaction: GetDbTransaction(tran));
    }
}
