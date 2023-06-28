using Dapper;
using PokedexDataAccess.Interfaces;
using PokedexModels.Model;
using System.Data;
using System.Data.SqlClient;

namespace PokedexDataAccess.DataAccess.Dapper;

public class PokedexVersionDapper : IPokedexVersionDataAccessService
{
    private readonly string _connectionString;

    public PokedexVersionDapper(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<IEnumerable<PokedexVersionModel>> Get(int versionId)
    {
        using IDbConnection conn = new SqlConnection(_connectionString);
        return await conn.QueryAsync<PokedexVersionModel>("sp_pokedex_GetPokedexVersions", new { versionId }, commandType: CommandType.StoredProcedure);
    }

    public async Task Insert(PokedexVersionModel version)
    {
        using IDbConnection conn = new SqlConnection(_connectionString);
        await conn.ExecuteAsync("sp_pokedex_InsertPokedexVersion", version, commandType: CommandType.StoredProcedure);
    }

    public async Task Update(PokedexVersionModel version)
    {
        using IDbConnection conn = new SqlConnection(_connectionString);
        await conn.ExecuteAsync("sp_pokedex_InsertPokedexVersion", version, commandType: CommandType.StoredProcedure);
    }

    public async Task Delete(int versionId)
    {
        using IDbConnection conn = new SqlConnection(_connectionString);
        await conn.ExecuteAsync("sp_pokedex_DeletePokedexVersion", versionId, commandType: CommandType.StoredProcedure);
    }
}
