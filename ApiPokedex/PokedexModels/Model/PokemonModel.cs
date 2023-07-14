namespace PokedexModels.Model;

public class PokemonModel
{
    public PokemonModel()
    {
        EvolvesTo = new List<PokemonModel>();
    }
    public int Id { get; set; }

    public string Name { get; set; }

    public int Height { get; set; }

    public double Weight { get; set; }

    public int GenerationNumber { get; set; }

    public PokemonModel? EvolvesFrom { get; set; }

    public IEnumerable<PokemonModel> EvolvesTo { get; set; }

    public IEnumerable<TypeModel> Types { get; set; }

    public IEnumerable<PokemonVersionModel> Versions { get; set; }

    public string ImageName { get; set; }
}