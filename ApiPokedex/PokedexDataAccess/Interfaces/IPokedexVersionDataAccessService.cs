using PokedexModels.Model;

namespace PokedexDataAccess.Interfaces;

public interface IPokedexVersionDataAccessService
{
    void Delete(int versionId);
    IEnumerable<PokedexVersionModel> Get(int versionId);
    void Insert(PokedexVersionModel version);
    void Update(PokedexVersionModel version);
}