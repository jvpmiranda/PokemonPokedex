using PokedexModels.Model;

namespace PokedexServices.Interfaces;

public interface IImageService
{
    ImageModel GetImage(int pokemonId);
}