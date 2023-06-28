namespace ApiPokedex.Contract.v1.Out;

public class OutFullPokemon : OutBasicPokemon
{
    public List<OutEvolutionPokemon> Evolutions { get; set; }

    public OutPreEvolutionPokemon PreEvolution { get; set; }
}

public class OutBasicPokemon : ErrorStatus
{
    public int Id { get; set; }

    public string Name { get; set; }

    public int Height { get; set; }

    public double Weight { get; set; }

    public int GenerationNumber { get; set; }

    public IEnumerable<OutGameVersion> Version { get; set; }

    public IEnumerable<OutTypeOfPokemon> Types { get; set; }

    public string ImageName { get; set; }
}


public class OutEvolutionPokemon : OutBasicPokemon
{
    public List<OutEvolutionPokemon> Evolutions { get; set; }
}


public class OutPreEvolutionPokemon : OutBasicPokemon
{
    public OutPreEvolutionPokemon PreEvolution { get; set; }
}
