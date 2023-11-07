using PokedexApiCaller.Contract.v1.Out;

namespace PokedexApiCaller.Model;

public class PokemonDescription
{
    public int VersionId { get; set; }

    public string VersionName { get; set; }

    public string Description { get; set; }

    public static implicit operator OutPokemonPokedexDescription(PokemonDescription descr)
    {
        var outDescription = new OutPokemonPokedexDescription();
        outDescription.VersionId = descr.VersionId;
        outDescription.VersionName = descr.VersionName;
        outDescription.Description = descr.Description;
        return outDescription;
    }

    public static implicit operator PokemonDescription(OutPokemonPokedexDescription descr)
    {
        var outDescription = new PokemonDescription();
        outDescription.VersionId = descr.VersionId;
        outDescription.VersionName = descr.VersionName;
        outDescription.Description = descr.Description;
        return outDescription;
    }
}