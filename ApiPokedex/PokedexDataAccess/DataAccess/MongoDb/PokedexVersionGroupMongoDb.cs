using MongoDB.Driver;
using PokedexDataAccess.DataAccess.MongoDb.Abstract;
using PokedexDataAccess.DataAccess.MongoDb.CollectionNames;
using PokedexDataAccess.Interfaces;
using PokedexDataAccess.Interfaces.Infrastructure;
using PokedexDataAccess.Model.MongoDb;
using PokedexModels.Model;
using System.Data;

namespace PokedexDataAccess.DataAccess.Dapper;

public class PokedexVersionGroupMongoDb : ConnectionMongo, IPokedexVersionGroupDataAccess
{
    public PokedexVersionGroupMongoDb(IMongoDatabase connection) : base(connection) { }

    public async Task Delete(int versionGroupId)
    {
        var collection = _conn.GetCollection<PokedexVersionGroupModelMongoDb>(MongoCollections.PokedexVersionsGroup);
        await collection.DeleteOneAsync(pok => pok.GroupId == versionGroupId);
    }

    public void DeleteInTransaction(int versionGroupId, IDataAccessTransaction tran)
    {
        var session = ((ConnectionMongo)tran).Session;
        var collection = _conn.GetCollection<PokedexVersionGroupModelMongoDb>(MongoCollections.PokedexVersionsGroup);
        collection.DeleteOne(session, pok => pok.GroupId == versionGroupId);
    }

    public async Task<IEnumerable<PokedexVersionGroupModel>> Get(int? versionGroupId)
    {
        var collection = _conn.GetCollection<PokedexVersionGroupModelMongoDb>(MongoCollections.PokedexVersionsGroup);

        IAsyncCursor<PokedexVersionGroupModelMongoDb> result;
        if (versionGroupId.HasValue && versionGroupId.Value > 0)
            result = await collection.FindAsync(pok => pok.GroupId == versionGroupId);
        else
            result = await collection.FindAsync(_ => true);

        return result.ToList().Select(x => MongoDbMapper.ToPokedexVersionGroupModel(x));
    }

    public IEnumerable<PokedexVersionGroupModel> GetInTransaction(int? versionGroupId, IDataAccessTransaction tran)
    {
        var session = ((ConnectionMongo)tran).Session;
        var collection = _conn.GetCollection<PokedexVersionGroupModelMongoDb>(MongoCollections.PokedexVersionsGroup);
        if (!versionGroupId.HasValue)
            versionGroupId = 0;
        var result = collection.Find(session, pok => pok.GroupId == versionGroupId || versionGroupId == 0);

        return result.ToList().Select(x => MongoDbMapper.ToPokedexVersionGroupModel(x));
    }

    public async Task Upsert(PokedexVersionGroupModel version)
    {
        var collection = _conn.GetCollection<PokedexVersionGroupModelMongoDb>(MongoCollections.PokedexVersionsGroup);
        await collection.ReplaceOneAsync(pok => pok.GroupId == version.GroupId, 
            MongoDbMapper.FromPokedexVersionGroupModel(version), 
            options: new ReplaceOptions() { IsUpsert = true});
    }

    public void UpsertInTransaction(PokedexVersionGroupModel version, IDataAccessTransaction tran)
    {
        var session = ((ConnectionMongo)tran).Session;
        var collection = _conn.GetCollection<PokedexVersionGroupModelMongoDb>(MongoCollections.PokedexVersionsGroup);
        collection.ReplaceOne(session,
            pok => pok.GroupId == version.GroupId,
            MongoDbMapper.FromPokedexVersionGroupModel(version),
            options: new ReplaceOptions() { IsUpsert = true });
    }
}
