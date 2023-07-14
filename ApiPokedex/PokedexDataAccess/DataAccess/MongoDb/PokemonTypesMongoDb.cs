using MongoDB.Driver;
using MongoDB.Driver.Linq;
using PokedexDataAccess.DataAccess.MongoDb.Abstract;
using PokedexDataAccess.DataAccess.MongoDb.CollectionNames;
using PokedexDataAccess.Interfaces;
using PokedexDataAccess.Interfaces.Infrastructure;
using PokedexDataAccess.Model.MongoDb;
using PokedexModels.Model;

namespace PokedexDataAccess.DataAccess.MongoDb;

public class PokemonTypesMongoDb : ConnectionMongo, IPokemonTypesDataAccess
{
    public PokemonTypesMongoDb(IMongoDatabase connection) : base(connection) { }

    public async Task Delete(int typeId)
    {
        var collection = _conn.GetCollection<TypeModelMongoDb>(MongoCollections.Types);
        await collection.DeleteOneAsync(type => type.Id == typeId);
    }

    public async Task DeleteInTransaction(int typeId, IDataAccessTransaction tran)
    {
        var session = ((ConnectionMongo)tran).Session;
        var collection = _conn.GetCollection<TypeModelMongoDb>(MongoCollections.Types);
        await collection.DeleteOneAsync(session, type => type.Id == typeId);
    }

    public async Task<IEnumerable<TypeModel>> Get(int? typeId)
    {
        var collection = _conn.GetCollection<TypeModelMongoDb>(MongoCollections.Types);
        IAsyncCursor<TypeModelMongoDb> result;
        if (typeId.HasValue)
            result = await collection.FindAsync(type => type.Id == typeId);
        else
            result = await collection.FindAsync(_ => true);

        return result.ToList().Select(x => MongoDbMapper.ToTypeModel(x));
    }

    public async Task<IEnumerable<TypeModel>> GetInTransaction(int? typeId, IDataAccessTransaction tran)
    {
        var session = ((ConnectionMongo)tran).Session;
        var collection = _conn.GetCollection<TypeModelMongoDb>(MongoCollections.Types);
        IAsyncCursor<TypeModelMongoDb> result;
        if (typeId.HasValue)
            result = await collection.FindAsync(session, type => type.Id == typeId);
        else
            result = await collection.FindAsync(session, _ => true);

        return result.ToList().Select(x => MongoDbMapper.ToTypeModel(x));
    }

    public async Task Upsert(IEnumerable<TypeModel> types)
    {
        foreach (var type in types)
            await Upsert(type);
    }

    public async Task UpsertInTransaction(IEnumerable<TypeModel> types, IDataAccessTransaction tran)
    {       
        foreach (var type in types)
            await UpsertInTransaction(type, tran);
    }

    public async Task Upsert(TypeModel type)
    {
        var collection = _conn.GetCollection<TypeModelMongoDb>(MongoCollections.Types);
        var typeDb = MongoDbMapper.FromTypeModel(type);
        await collection.ReplaceOneAsync(t => t.Id == typeDb.Id, typeDb, options: new ReplaceOptions() { IsUpsert = true });
    }

    public async Task UpsertInTransaction(TypeModel type, IDataAccessTransaction tran)
    {
        var session = ((ConnectionMongo)tran).Session;
        var collection = _conn.GetCollection<TypeModelMongoDb>(MongoCollections.Types);
        var typeDb = MongoDbMapper.FromTypeModel(type);
        await collection.ReplaceOneAsync(session, t => t.Id == typeDb.Id, typeDb, options: new ReplaceOptions() { IsUpsert = true });
    }
}
