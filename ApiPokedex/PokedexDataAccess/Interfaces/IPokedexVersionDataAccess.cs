using PokedexDataAccess.Interfaces.Infrastructure;
using PokedexModels.Model;

namespace PokedexDataAccess.Interfaces;

public interface IPokedexVersionDataAccess : IDataAccessTransaction
{
    Task<IEnumerable<PokedexVersionModel>> Get(int? versionId);
    IEnumerable<PokedexVersionModel> GetInTransaction(int? versionId, IDataAccessTransaction tran);
    Task Delete(int versionId);
    void DeleteInTransaction(int versionId, IDataAccessTransaction tran);
    Task Upsert(PokedexVersionModel version);
    void UpsertInTransaction(PokedexVersionModel version, IDataAccessTransaction tran);
}