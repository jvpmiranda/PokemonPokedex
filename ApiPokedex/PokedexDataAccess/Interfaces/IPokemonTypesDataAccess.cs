using PokedexDataAccess.Interfaces.Infrastructure;
using PokedexModels.Model;

namespace PokedexDataAccess.Interfaces;

public interface IPokemonTypesDataAccess : IDataAccessTransaction
{
    Task Delete(int typeId);
    Task DeleteInTransaction(int typeId, IDataAccessTransaction tran);
    Task<IEnumerable<TypeModel>> Get(int? typeId);
    Task<IEnumerable<TypeModel>> GetInTransaction(int? typeId, IDataAccessTransaction tran);
    Task Upsert(IEnumerable<TypeModel> types);
    Task UpsertInTransaction(IEnumerable<TypeModel> types, IDataAccessTransaction tran);
    Task Upsert(TypeModel type);
    Task UpsertInTransaction(TypeModel type, IDataAccessTransaction tran);
}