using DapperConnection.DataAccess;
using PokedexDataAccess.Interfaces;
using PokedexModels.Model;

namespace PokedexDataAccess.DataAccess.Dapper;

public class PokedexVersionDapper : IPokedexVersionDataAccessService
{
    protected readonly ISqlDapperDataAccess _sql;

    public PokedexVersionDapper(ISqlDapperDataAccess sql)
    {
        _sql = sql;
    }

    public IEnumerable<PokedexVersionModel> Get(int versionId)
    {
        return _sql.ExecuteQueryStoredProcedure<PokedexVersionModel, PokedexVersionGroupModel, PokedexVersionModel, dynamic>("sp_pokedex_GetPokedexVersions", 
            (version, group) =>
            {
                version.VersionGroup = group;
                return version;
            },
            new { versionId },
            splitOn: "Id").Result;
    }

    public void Insert(PokedexVersionModel version)
    {
        _sql.ExecuteNonQueryStoredProcedure("sp_pokedex_InsertPokedexVersion", version);
    }

    public void Update(PokedexVersionModel version)
    {
        _sql.ExecuteNonQueryStoredProcedure("sp_pokedex_UpdatePokedexVersion", version);
    }

    public void Delete(int versionId)
    {
        _sql.ExecuteNonQueryStoredProcedure("sp_pokedex_DeletePokedexVersion", new { versionId });
    }
}
