using PokedexModels.Model;

namespace PokedexServices.Interfaces;

public interface IPokedexVersionService
{
    Task Delete(int versionId);
    Task<IEnumerable<PokedexVersionModel>> Get(int? versionId);
    Task Insert(PokedexVersionModel version);
    Task Update(PokedexVersionModel version);
}