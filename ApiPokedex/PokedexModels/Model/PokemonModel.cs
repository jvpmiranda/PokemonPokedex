namespace PokedexModels.Model;

public class PokemonModel
{
    public PokemonModel()
    {
        Types = new List<TypeModel>();
        EvolvesTo = new List<int>();
        Description = string.Empty;
    }

    public int Id { get; set; }

    public string Identifier { get; set; } = null!;

    public int Height { get; set; }

    public double Weight { get; set; }

    public int GenerationNumber { get; set; }

    public int? EvolvesFrom { get; set; }

    public List<int> EvolvesTo { get; set; }

    public ICollection<TypeModel> Types { get; set; }

    public string Description { get; set; }
}