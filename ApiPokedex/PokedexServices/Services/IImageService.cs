using PokedexDataAccess.Interfaces;
using PokedexModels.Model;
using PokedexServices.Interfaces;

namespace PokedexServices.Services;

public class ImageService : IImageService
{
    protected readonly IImageDataAccessService _dataAccess;

    public ImageService(IImageDataAccessService dataAccess)
    {
        _dataAccess = dataAccess;
    }

    public ImageModel GetImage(int pokemonId)
    {
        return _dataAccess.GetImage(pokemonId).Result;
    }
}
