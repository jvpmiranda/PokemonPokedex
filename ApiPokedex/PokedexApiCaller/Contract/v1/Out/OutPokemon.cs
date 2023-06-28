namespace PokedexApiCaller.Contract.v1.Out;

public class OutPokemon
{
    public int Id { get; set; }

    public string Name { get; set; }

    public int Height { get; set; }

    public double Weight { get; set; }

    public int? EvolvesFrom { get; set; }

    public int GenerationNumber { get; set; }

    public IEnumerable<int> EvolvesTo { get; set; }

    public IEnumerable<OutTypeOfPokemon> Types { get; set; }

    public IEnumerable<OutGameVersion> Version { get; set; }

    public string ImageName { get; set; }
}
