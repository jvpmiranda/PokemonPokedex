namespace PokedexModels.Model;

public class PokemonModelFull
{
    public PokemonModelFull()
    {
        EvolvesTo = new List<PokemonModelFull>();
    }

    public PokemonModel Pokemon { get; set; }
    public PokemonModelFull EvolvesFrom { get; set; }
    public List<PokemonModelFull> EvolvesTo { get; set; }
}
