namespace ApiPokedex.Contract.v1.Out;

public class OutBasicPokemon
{
    public int Id { get; set; }

    public string Name { get; set; }

    public int Height { get; set; }

    public double Weight { get; set; }

    public int GenerationNumber { get; set; }

    public string Description { get; set; }

    public IEnumerable<OutTypeOfPokemon> Types { get; set; }
}

public class OutFullPokemon : OutBasicPokemon
{
    public OutFullPokemon()
    {
        Evolutions = new List<OutEvolutionPokemon>();
    }

    public List<OutEvolutionPokemon> Evolutions { get; set; }

    public OutPreEvolutionPokemon PreEvolution { get; set; }
}


public class OutEvolutionPokemon : OutBasicPokemon
{
    public List<OutEvolutionPokemon> Evolutions { get; set; }
}


public class OutPreEvolutionPokemon : OutBasicPokemon
{
    public OutPreEvolutionPokemon PreEvolution { get; set; }
}

