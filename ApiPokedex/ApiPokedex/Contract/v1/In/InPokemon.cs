namespace ApiPokedex.Contract.v1.In;

public class InPokemon
{
    public int Id { get; set; }

    public string Name { get; set; }

    public int Height { get; set; }

    public double Weight { get; set; }

    public int GenerationNumber { get; set; }

    public int? EvolvesFrom { get; set; }

    public IEnumerable<int> EvolvesTo { get; set; }

    public IEnumerable<InType> Types { get; set; }

    public IEnumerable<InAvailableVersions> AvailableVersions { get; set; }
}

public class InAvailableVersions
{
    public int Id { get; set; }

    public int Name { get; set; }
}
public class InType
{
    public int Id { get; set; }

    public int Name { get; set; }
}