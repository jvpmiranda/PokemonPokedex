using MongoDB.Driver;
using PokedexDataAccess.DataAccess.MongoDb.Abstract;
using PokedexDataAccess.DataAccess.MongoDb.CollectionNames;
using PokedexDataAccess.Interfaces;
using PokedexDataAccess.Interfaces.Infrastructure;
using PokedexDataAccess.Model.MongoDb;
using PokedexModels.Model;

namespace PokedexDataAccess.DataAccess.MongoDb;

public class ImageMongoDb : ConnectionMongo, IImageDataAccess
{
    public ImageMongoDb(IMongoDatabase connection) : base(connection) { }

    public async Task<ImageModel> GetImage(int pokemonId)
    {
        var collection = _conn.GetCollection<ImageModelMongoDb>(MongoCollections.Images);
        var result = await collection.Find(img => img.Id == pokemonId).FirstOrDefaultAsync();
        if (result == null)
            return new ImageModel();
        return MongoDbMapper.ToImageModel(result);
    }

    public async Task<ImageModel> GetImageInTransaction(int pokemonId, IDataAccessTransaction tran)
    {
        var collection = _conn.GetCollection<ImageModelMongoDb>(MongoCollections.Images);
        var result = await collection.Find(((ConnectionMongo)tran).Session, img => img.Id == pokemonId).FirstOrDefaultAsync();
        if (result == null)
            return new ImageModel();
        return MongoDbMapper.ToImageModel(result);
    }

    public async Task Insert(ImageModel image)
    {
        var collection = _conn.GetCollection<ImageModelMongoDb>(MongoCollections.Images);
        await collection.InsertOneAsync(MongoDbMapper.FromImageModel(image));
    }

    public async Task InsertInTransaction(ImageModel image, IDataAccessTransaction tran)
    {
        var collection = _conn.GetCollection<ImageModelMongoDb>(MongoCollections.Images);
        await collection.InsertOneAsync(((ConnectionMongo)tran).Session, MongoDbMapper.FromImageModel(image));
    }

    public async Task Update(ImageModel image)
    {
        var collection = _conn.GetCollection<ImageModelMongoDb>(MongoCollections.Images);
        await collection.FindOneAndReplaceAsync(pok => pok.Id == image.PokemonId, MongoDbMapper.FromImageModel(image));
    }
    
    public async Task UpdateInTransaction(ImageModel image, IDataAccessTransaction tran)
    {
        var collection = _conn.GetCollection<ImageModelMongoDb>(MongoCollections.Images);
        await collection.FindOneAndReplaceAsync(
            session: ((ConnectionMongo)tran).Session, 
            filter: pok => pok.Id == image.PokemonId, 
            replacement: MongoDbMapper.FromImageModel(image));
    }

    public async Task Delete(int pokemonId)
    {
        var collection = _conn.GetCollection<ImageModelMongoDb>(MongoCollections.Images);
        await collection.DeleteOneAsync(pok => pok.Id == pokemonId);
    }

    public async Task DeleteInTransaction(int pokemonId, IDataAccessTransaction tran)
    {
        var collection = _conn.GetCollection<ImageModelMongoDb>(MongoCollections.Images);
        await collection.DeleteOneAsync(((ConnectionMongo)tran).Session, pok => pok.Id == pokemonId);
    }
}
