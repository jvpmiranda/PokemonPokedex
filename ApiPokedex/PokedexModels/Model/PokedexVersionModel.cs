namespace PokedexModels.Model;

public class PokedexVersionModel
{
    public int Id { get; set; }

    public string Name { get; set; }

    public PokedexVersionGroupModel VersionGroup { get; set; }

}