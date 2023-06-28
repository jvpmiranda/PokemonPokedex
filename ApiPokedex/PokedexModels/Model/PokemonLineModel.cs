namespace PokedexModels.Model;

public class PokemonLineModel : PokemonModel
{
    public PokemonLineModel() => Evolutions = new List<PokemonLineModel>();

    public PokemonLineModel PreEvolution { get; set; }
    public List<PokemonLineModel> Evolutions { get; set; }
}
