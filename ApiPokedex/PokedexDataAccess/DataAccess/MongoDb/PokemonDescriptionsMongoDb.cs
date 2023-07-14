using MongoDB.Driver;
using PokedexDataAccess.DataAccess.MongoDb.Abstract;
using PokedexDataAccess.DataAccess.MongoDb.CollectionNames;
using PokedexDataAccess.Interfaces.Infrastructure;
using PokedexDataAccess.Model.MongoDb;
using PokedexModels.Model;
using PokedexServices.Interfaces;

namespace PokedexDataAccess.DataAccess.MongoDb;

public class PokemonDescriptionsMongoDb : ConnectionMongo, IPokemonDescriptionsDataAccess
{
    public PokemonDescriptionsMongoDb(IMongoDatabase connection) : base(connection) { }

    public async Task Delete(int pokemonId, int versionId)
    {
        var collection = _conn.GetCollection<PokemonModelMongoDb>(MongoCollections.Pokemons);
        var filter = Builders<PokemonModelMongoDb>.Filter.Eq(p => p.Id, pokemonId) & Builders<PokemonModelMongoDb>.Filter.Eq("Versions._id", versionId);
        var update = Builders<PokemonModelMongoDb>.Update.Set(p => p.Versions[-1], null);

        await collection.UpdateOneAsync(filter, update);
    }

    public async Task DeleteInTransaction(int pokemonId, int versionId, IDataAccessTransaction tran)
    {
        var session = ((ConnectionMongo)tran).Session;
        var collection = _conn.GetCollection<PokemonModelMongoDb>(MongoCollections.Pokemons);
        var filter = Builders<PokemonModelMongoDb>.Filter.Eq(p => p.Id, pokemonId) & Builders<PokemonModelMongoDb>.Filter.Eq("Versions._id", versionId);
        var update = Builders<PokemonModelMongoDb>.Update.Set(p => p.Versions[-1], null);

        await collection.UpdateOneAsync(session, filter, update);
    }

    public async Task<IEnumerable<PokemonVersionModel>> Get(int pokemonId, int? versionId)
    {
        var collection = _conn.GetCollection<PokemonModelMongoDb>(MongoCollections.Pokemons);

        var filter = Builders<PokemonModelMongoDb>.Filter.Eq(p => p.Id, pokemonId);
        if (versionId.HasValue)
            filter = filter & Builders<PokemonModelMongoDb>.Filter.ElemMatch(v => v.Versions, ver => ver.VersionId == versionId);

        var result = await collection.FindAsync(filter);

        if (!result.Any())
            return Enumerable.Empty<PokemonVersionModel>();

        return result.ToList().First().Versions.Select(x => MongoDbMapper.ToPokemonVersionModel(x));
    }

    public async Task<IEnumerable<PokemonVersionModel>> GetInTransaction(int pokemonId, int? versionId, IDataAccessTransaction tran)
    {
        var session = ((ConnectionMongo)tran).Session;
        var collection = _conn.GetCollection<PokemonModelMongoDb>(MongoCollections.Pokemons);

        var filter = Builders<PokemonModelMongoDb>.Filter.Eq(p => p.Id, pokemonId);
        if (versionId.HasValue)
            filter = filter & Builders<PokemonModelMongoDb>.Filter.ElemMatch(v => v.Versions, ver => ver.VersionId == versionId);

        var result = await collection.FindAsync(session, filter);

        if (result == null)
            return Enumerable.Empty<PokemonVersionModel>();

        return result.ToList().First().Versions.Select(x => MongoDbMapper.ToPokemonVersionModel(x));
    }

    public async Task Upsert(int pokemonId, PokemonVersionModel version)
    {
        var collection = _conn.GetCollection<PokemonModelMongoDb>(MongoCollections.Pokemons);
        var versionDb = MongoDbMapper.FromPokemonVersionModel(version);

        var filter = Builders<PokemonModelMongoDb>.Filter.Eq(p => p.Id, pokemonId)
            & Builders<PokemonModelMongoDb>.Filter.ElemMatch(v => v.Versions, ver => ver.VersionId == versionDb.VersionId);

        var update = Builders<PokemonModelMongoDb>.Update.Set(e => e.Versions[-1], versionDb);

        var options = new FindOneAndUpdateOptions<PokemonModelMongoDb>() { ReturnDocument = ReturnDocument.Before };

        var result = await collection.FindOneAndUpdateAsync(filter, update, options);

        if (result == null)
        { 
            update = Builders<PokemonModelMongoDb>.Update.Push(v => v.Versions, versionDb);
            await collection.UpdateOneAsync(p => p.Id == pokemonId, update);
        }
    }

    public async Task UpsertInTransaction(int pokemonId, PokemonVersionModel version, IDataAccessTransaction tran)
    {
        var session = ((ConnectionMongo)tran).Session;
        var collection = _conn.GetCollection<PokemonModelMongoDb>(MongoCollections.Pokemons);
        var versionDb = MongoDbMapper.FromPokemonVersionModel(version);

        var filter = Builders<PokemonModelMongoDb>.Filter.Eq(p => p.Id, pokemonId)
            & Builders<PokemonModelMongoDb>.Filter.ElemMatch(v => v.Versions, ver => ver.VersionId == versionDb.VersionId);

        var update = Builders<PokemonModelMongoDb>.Update.Set(e => e.Versions[-1], versionDb);

        var options = new FindOneAndUpdateOptions<PokemonModelMongoDb>() { ReturnDocument = ReturnDocument.Before };

        var result = await collection.FindOneAndUpdateAsync(session, filter, update, options);

        if (result == null)
        {
            update = Builders<PokemonModelMongoDb>.Update.Push(v => v.Versions, versionDb);
            await collection.UpdateOneAsync(session, p => p.Id == pokemonId, update);
        }
        //var collection = _conn.GetCollection<PokemonModelMongoDb>(MongoCollections.Pokemons);
        //var versionDb = MongoDbMapper.FromPokemonVersionModel(version);
        //var filter = Builders<PokemonModelMongoDb>.Filter.Eq(p => p.Id, pokemonId);

        //var update = Builders<PokemonModelMongoDb>.Update.Push(v => v.Versions, versionDb);

        //await collection.UpdateOneAsync(session, filter, update);
    }
    
}
