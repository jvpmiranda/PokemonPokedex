namespace PokedexModels.Model;

public class PokemonDescription
{
    public int Id { get; set; }

    public string Description { get; set; } = null!;

    public string VersionName { get; set; } = null!;
}