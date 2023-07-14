namespace PokedexModels.Model;

public class PokemonVersionModel
{
    public int VersionId { get; set; }

    public string VersionName { get; set; }

    public string Description { get; set; }

    public PokemonVersionGroupModel VersionGroup { get; set; }
}