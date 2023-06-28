using PokedexDataAccess.Interfaces;
using PokedexModels.Model;
using PokedexServices.Interfaces;
using System;

namespace PokedexServices.Services;

public class PokedexVersionService : IPokedexVersionService
{
    protected readonly IPokedexVersionDataAccessService _dataAccess;

    public PokedexVersionService(IPokedexVersionDataAccessService dataAccess)
    {
        _dataAccess = dataAccess;
    }

    public IEnumerable<PokedexVersionModel> Get(int? versionId)
    {
        int value = versionId.HasValue ? versionId.Value : 0;
        return _dataAccess.Get(value).Result;
    }

    public void Delete(int versionId)
    {
        _dataAccess.Delete(versionId);
    }

    public void Insert(PokedexVersionModel version)
    {
        _dataAccess.Insert(version);
    }

    public void Update(PokedexVersionModel version)
    {
        _dataAccess.Update(version);
    }

}
