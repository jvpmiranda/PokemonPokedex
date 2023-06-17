namespace PokedexModels.Model;

public class PokemonModel
{
    public PokemonModel()
    {
        EvolvesTo = new List<int>();
    }

    public int Id { get; set; }

    public string Name { get; set; }

    public int Height { get; set; }

    public double Weight { get; set; }

    public int GenerationNumber { get; set; }

    public int? EvolvesFrom { get; set; }

    public IEnumerable<int> EvolvesTo { get; set; }

    public IEnumerable<TypeModel> Types { get; set; }

    public string Description { get; set; }
}