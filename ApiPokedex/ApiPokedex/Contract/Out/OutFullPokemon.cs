namespace ApiPokedex.Contract.Out;

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
        EvolvesTo = new List<OutEvolutionPokemon>();
    }

    public List<OutEvolutionPokemon> EvolvesTo { get; set; }

    public OutPreEvolutionPokemon EvolvesFrom { get; set; }
}


public class OutEvolutionPokemon : OutBasicPokemon
{
    public List<OutEvolutionPokemon> EvolvesTo { get; set; }
}


public class OutPreEvolutionPokemon : OutBasicPokemon
{
    public OutPreEvolutionPokemon EvolvesFrom { get; set; }
}

