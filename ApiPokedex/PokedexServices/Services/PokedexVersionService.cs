using PokedexDataAccess.Interfaces;
using PokedexModels.Model;
using PokedexServices.Interfaces;
using System;

namespace PokedexServices.Services;

public class PokedexVersionService : IPokedexVersionService
{
    private readonly IPokedexVersionDataAccess _pokedexVersion;
    private readonly IPokedexVersionGroupDataAccess _pokedexVersionGroup;

    public PokedexVersionService(IPokedexVersionDataAccess pokedexVersion, IPokedexVersionGroupDataAccess pokedexVersionGroup)
    {
        _pokedexVersion = pokedexVersion;
        _pokedexVersionGroup = pokedexVersionGroup;
    }

    public async Task<IEnumerable<PokedexVersionModel>> Get(int? versionId)
    {
        return await _pokedexVersion.Get(versionId);
    }

    public async Task Delete(int versionId)
    {
        await _pokedexVersion.Delete(versionId);
    }

    public async Task Insert(PokedexVersionModel version)
    {
        await _pokedexVersion.UseTransaction(tran =>
        {
            _pokedexVersionGroup.UpsertInTransaction(version.VersionGroup, tran);
            _pokedexVersion.UpsertInTransaction(version, tran);
            return true;
        }
        );
    }

    public async Task Update(PokedexVersionModel version)
    {
        await _pokedexVersion.Upsert(version);
    }
}
