using PokedexDataAccess.Interfaces.Infrastructure;
using PokedexModels.Model;

namespace PokedexDataAccess.Interfaces;

public interface IPokedexVersionGroupDataAccess : IDataAccessTransaction
{
    Task<IEnumerable<PokedexVersionGroupModel>> Get(int? versionGroupId);
    IEnumerable<PokedexVersionGroupModel> GetInTransaction(int? versionGroupId, IDataAccessTransaction tran);
    Task Delete(int versionGroupId);
    void DeleteInTransaction(int versionGroupId, IDataAccessTransaction tran);
    Task Upsert(PokedexVersionGroupModel versionGroup);
    void UpsertInTransaction(PokedexVersionGroupModel versionGroup, IDataAccessTransaction tran);
}