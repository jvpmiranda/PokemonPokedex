using MongoDB.Driver;
using MongoDB.Driver.Linq;
using PokedexDataAccess.DataAccess.MongoDb.Abstract;
using PokedexDataAccess.DataAccess.MongoDb.CollectionNames;
using PokedexDataAccess.Interfaces;
using PokedexDataAccess.Interfaces.Infrastructure;
using PokedexDataAccess.Model.MongoDb;
using PokedexModels.Model;
using System.Data;

namespace PokedexDataAccess.DataAccess.Dapper;

public class PokemonMongoDb : ConnectionMongo, IPokemonDataAccess
{
    public PokemonMongoDb(IMongoDatabase connection) : base(connection) { }

    public async Task<IEnumerable<PokemonModel>> GetBasicPokemon(int? pokemonId)
    {
        var collection = _conn.GetCollection<PokemonModelMongoDb>(MongoCollections.Pokemons);

        IAsyncCursor<PokemonModelMongoDb> result;
        if (pokemonId.HasValue)
            result = await collection.FindAsync(pok => pok.Id == pokemonId);
        else
            result = await collection.FindAsync(_ => true);

        return result.ToList().Select(x => MongoDbMapper.ToPokemonModel(x));
    }

    public async Task<IEnumerable<PokemonModel>> GetBasicPokemonInTransaction(int? pokemonId, IDataAccessTransaction tran)
    {
        var collection = _conn.GetCollection<PokemonModelMongoDb>(MongoCollections.Pokemons);

        IAsyncCursor<PokemonModelMongoDb> result;
        if (pokemonId.HasValue)
            result = await collection.FindAsync(((ConnectionMongo)tran).Session, pok => pok.Id == pokemonId);
        else
            result = await collection.FindAsync(((ConnectionMongo)tran).Session, _ => true);

        return result.ToList().Select(x => MongoDbMapper.ToPokemonModel(x));
    }

    public async Task<PokemonModel> GetPokemon(int pokemonId, int versionId)
    {
        var collection = _conn.GetCollection<PokemonModelMongoDb>(MongoCollections.Pokemons);

        var filter = Builders<PokemonModelMongoDb>.Filter.Eq(p => p.Id, pokemonId);

        if (versionId > 0)
            filter = filter & Builders<PokemonModelMongoDb>.Filter.ElemMatch(v => v.Versions, ver => ver.VersionId == versionId);

        var result = await collection.Find(filter).FirstOrDefaultAsync();

        if (result == null)
            return new PokemonModel();

        if (versionId > 0)
            result.Versions = result.Versions.Where(v => v.VersionId == versionId).ToList(); 

        return MongoDbMapper.ToPokemonModel(result);
    }

    public async Task<PokemonModel> GetPokemonInTransaction(int pokemonId, int versionId, IDataAccessTransaction tran)
    {
        var collection = _conn.GetCollection<PokemonModelMongoDb>(MongoCollections.Pokemons);

        var result = await collection.Aggregate(((ConnectionMongo)tran).Session)
            .Match(x => x.Id == pokemonId)
            .Match(x => x.Versions.Any(v => v.VersionId == versionId || versionId == 0))
            .FirstOrDefaultAsync();

        if (result == null)
            return new PokemonModel();
        return MongoDbMapper.ToPokemonModel(result);
    }

    public async Task Insert(PokemonModel pokemon)
    {
        var collection = _conn.GetCollection<PokemonModelMongoDb>(MongoCollections.Pokemons);
        await collection.InsertOneAsync(MongoDbMapper.FromPokemonModel(pokemon));
    }

    public async Task InsertInTransaction(PokemonModel pokemon, IDataAccessTransaction tran)
    {
        var collection = _conn.GetCollection<PokemonModelMongoDb>(MongoCollections.Pokemons);
        await collection.InsertOneAsync(((ConnectionMongo)tran).Session, MongoDbMapper.FromPokemonModel(pokemon));
    }

    public async Task Update(PokemonModel pokemon)
    {
        var collection = _conn.GetCollection<PokemonModelMongoDb>(MongoCollections.Pokemons);
        var pokemonDb = MongoDbMapper.FromPokemonModel(pokemon);

        await collection.FindOneAndReplaceAsync(pok => pok.Id == pokemon.Id, pokemonDb); 
    }

    public async Task UpdateInTransaction(PokemonModel pokemon, IDataAccessTransaction tran)
    {
        var session = ((ConnectionMongo)tran).Session;
        var collection = _conn.GetCollection<PokemonModelMongoDb>(MongoCollections.Pokemons);
        var pokemonDb = MongoDbMapper.FromPokemonModel(pokemon);

        await collection.FindOneAndReplaceAsync(
            session: ((ConnectionMongo)tran).Session,
            filter: pok => pok.Id == pokemon.Id,
            replacement: MongoDbMapper.FromPokemonModel(pokemon));
    }

    public async Task Delete(int pokemonId)
    {
        var collection = _conn.GetCollection<PokemonModelMongoDb>(MongoCollections.Pokemons);
        await collection.DeleteOneAsync(pok => pok.Id == pokemonId);
    }
    public async Task DeleteInTransaction(int pokemonId, IDataAccessTransaction tran)
    {
        var collection = _conn.GetCollection<PokemonModelMongoDb>(MongoCollections.Pokemons);
        await collection.DeleteOneAsync(((ConnectionMongo)tran).Session, pok => pok.Id == pokemonId);
    }
}
