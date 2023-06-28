using PokedexModels.Model;

namespace PokedexDataAccess.Interfaces;

public interface IPokedexVersionDataAccessService
{
    Task Delete(int versionId);
    Task<IEnumerable<PokedexVersionModel>> Get(int versionId);
    Task Insert(PokedexVersionModel version);
    Task Update(PokedexVersionModel version);
}