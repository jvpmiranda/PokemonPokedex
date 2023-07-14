using PokedexModels.Model;

namespace PokedexServices.Interfaces;

public interface IImageService
{
    Task Delete(int pokemonId);
    Task<ImageModel> GetImage(int pokemonId);
    Task Insert(ImageModel image);
    Task Update(ImageModel image);
}