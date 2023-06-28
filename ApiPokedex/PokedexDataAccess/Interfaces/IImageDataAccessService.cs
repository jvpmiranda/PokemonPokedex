using PokedexModels.Model;

namespace PokedexDataAccess.Interfaces;

public interface IImageDataAccessService
{
    Task<ImageModel> GetImage(int pokemonId);
}