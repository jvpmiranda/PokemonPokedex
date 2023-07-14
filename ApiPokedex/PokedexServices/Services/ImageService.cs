using PokedexDataAccess.Interfaces;
using PokedexModels.Model;
using PokedexServices.Interfaces;

namespace PokedexServices.Services;

public class ImageService : IImageService
{
    protected readonly IImageDataAccess _dataAccess;

    public ImageService(IImageDataAccess dataAccess)
    {
        _dataAccess = dataAccess;
    }

    public async Task<ImageModel> GetImage(int pokemonId)
    {
        return _dataAccess.GetImage(pokemonId).Result;
    }

    public async Task Delete(int pokemonId)
    {
        await _dataAccess.Delete(pokemonId);
    }

    public async Task Insert(ImageModel image)
    {
        await _dataAccess.Insert(image);
    }

    public async Task Update(ImageModel image)
    {
        await _dataAccess.Update(image);
    }
}
