using PokedexApiCaller.Contract.v1.Out;

namespace PokedexApiCaller.Model;

public class PokemonTypes
{
    public int Id { get; set; }

    public string Name { get; set; }

    public static implicit operator OutTypeOfPokemon(PokemonTypes type)
    {
        var outType = new OutTypeOfPokemon();
        outType.Id = type.Id;
        outType.Name = type.Name;
        return outType;
    }

    public static implicit operator PokemonTypes(OutTypeOfPokemon type)
    {
        var outType = new PokemonTypes();
        outType.Id = type.Id;
        outType.Name = type.Name;
        return outType;
    }
}