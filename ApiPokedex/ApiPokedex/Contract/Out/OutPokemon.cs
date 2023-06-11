namespace ApiPokedex.Contract.Out;

public class OutPokemon
{
    public int Id { get; set; }

    public string Name { get; set; }

    public int Height { get; set; }

    public double Weight { get; set; }

    public int? EvolvesFromSpeciesId { get; set; }

    public int GenerationId { get; set; }

    public IEnumerable<int> EvolvesTo { get; set;}

    public IEnumerable<OutTypeOfPokemon> Types { get; set; }

    public string Description { get; set; }
}