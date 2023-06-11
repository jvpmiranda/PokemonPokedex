using PokedexModels.Model;

namespace PokedexServices.Interfaces;

public interface IPokedexVersionService
{
    void Delete(int versionId);
    IEnumerable<PokedexVersionModel> Get(int? versionId);
    void Insert(PokedexVersionModel version);
    void Update(PokedexVersionModel version);
}