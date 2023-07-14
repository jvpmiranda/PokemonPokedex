using PokedexDataAccess.Interfaces.Infrastructure;
using PokedexModels.Model;

namespace PokedexDataAccess.Interfaces;

public interface IImageDataAccess : IDataAccessTransaction
{
    Task<ImageModel> GetImage(int pokemonId);
    Task<ImageModel> GetImageInTransaction(int pokemonId, IDataAccessTransaction tran);
    Task Delete(int pokemonId);
    Task DeleteInTransaction(int pokemonId, IDataAccessTransaction tran);
    Task Insert(ImageModel image);
    Task InsertInTransaction(ImageModel image, IDataAccessTransaction tran);
    Task Update(ImageModel image);
    Task UpdateInTransaction(ImageModel image, IDataAccessTransaction tran);
}