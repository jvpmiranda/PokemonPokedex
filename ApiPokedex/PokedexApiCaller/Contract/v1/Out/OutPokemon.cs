namespace PokedexApiCaller.Contract.v1.Out;

public class OutPokemon : OutPokemonInfo
{
    public List<OutEvolutionPokemon> EvolvesTo { get; set; }

    public OutPreEvolutionPokemon EvolvesFrom { get; set; }
}

public class OutPokemonInfo : ErrorStatus
{
    public int Id { get; set; }

    public string Name { get; set; }

    public int Height { get; set; }

    public double Weight { get; set; }

    public int GenerationNumber { get; set; }

    public IEnumerable<OutPokemonVersion> Versions { get; set; }

    public IEnumerable<OutTypeOfPokemon> Types { get; set; }

    public string ImageName { get; set; }
}

public class OutTypeOfPokemon
{
    public int Id { get; set; }

    public string Name { get; set; }
}

public class OutPokemonVersion
{
    public int VersionId { get; set; }

    public string VersionName { get; set; }

    public string Description { get; set; }
}

public class OutEvolutionPokemon : OutPokemonInfo
{
    public List<OutEvolutionPokemon> EvolvesTo { get; set; }
}

public class OutPreEvolutionPokemon : OutPokemonInfo
{
    public OutPreEvolutionPokemon EvolvesFrom { get; set; }
}
