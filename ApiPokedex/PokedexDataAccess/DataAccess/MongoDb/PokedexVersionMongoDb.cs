using MongoDB.Driver;
using PokedexDataAccess.DataAccess.MongoDb.Abstract;
using PokedexDataAccess.DataAccess.MongoDb.CollectionNames;
using PokedexDataAccess.Interfaces;
using PokedexDataAccess.Interfaces.Infrastructure;
using PokedexDataAccess.Model.MongoDb;
using PokedexModels.Model;
using System.Data;

namespace PokedexDataAccess.DataAccess.Dapper;

public class PokedexVersionMongoDb : ConnectionMongo, IPokedexVersionDataAccess
{
    public PokedexVersionMongoDb(IMongoDatabase connection) : base(connection) { }

    public async Task Delete(int versionId)
    {
        var collection = _conn.GetCollection<PokedexVersionModelMongoDb>(MongoCollections.PokedexVersions);
        await collection.DeleteOneAsync(pok => pok.Id == versionId);
    }

    public void DeleteInTransaction(int versionId, IDataAccessTransaction tran)
    {
        var collection = _conn.GetCollection<PokedexVersionModelMongoDb>(MongoCollections.PokedexVersions);
        collection.DeleteOne(((ConnectionMongo)tran).Session, pok => pok.Id == versionId);
    }

    public async Task<IEnumerable<PokedexVersionModel>> Get(int? versionId)
    {
        var collection = _conn.GetCollection<PokedexVersionModelMongoDb>(MongoCollections.PokedexVersions);

        IAsyncCursor<PokedexVersionModelMongoDb> result;
        if (versionId.HasValue && versionId.Value > 0)
            result = await collection.FindAsync(pok => pok.Id == versionId);
        else
            result = await collection.FindAsync(_ => true);

        return result.ToList().Select(x => MongoDbMapper.ToPokedexVersionModel(x));
    }

    public IEnumerable<PokedexVersionModel> GetInTransaction(int? versionId, IDataAccessTransaction tran)
    {
        var collection = _conn.GetCollection<PokedexVersionModelMongoDb>(MongoCollections.PokedexVersions);

        if (!versionId.HasValue)
            versionId = 0;
        var result = collection.Find(((ConnectionMongo)tran).Session, pok => pok.Id == versionId || versionId == 0);

        return result.ToList().Select(x => MongoDbMapper.ToPokedexVersionModel(x));
    }

    public async Task Upsert(PokedexVersionModel version)
    {
        var collection = _conn.GetCollection<PokedexVersionModelMongoDb>(MongoCollections.PokedexVersions);
        await collection.ReplaceOneAsync(
            pok => pok.Id == version.Id,
            MongoDbMapper.FromPokedexVersionModel(version),
            options: new ReplaceOptions() { IsUpsert = true });
    }

    public void UpsertInTransaction(PokedexVersionModel version, IDataAccessTransaction tran)
    {
        var collection = _conn.GetCollection<PokedexVersionModelMongoDb>(MongoCollections.PokedexVersions);
        collection.ReplaceOne(
            session: ((ConnectionMongo)tran).Session,
            filter: pok => pok.Id == version.Id,
            replacement: MongoDbMapper.FromPokedexVersionModel(version),
            options: new ReplaceOptions() { IsUpsert = true });
    }
}
