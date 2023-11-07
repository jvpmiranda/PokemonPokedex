namespace PokedexApiCaller.Contract.v1.In;

public class InPokemon
{
    public int Id { get; set; }

    public string Name { get; set; }

    public int Height { get; set; }

    public double Weight { get; set; }

    public int GenerationNumber { get; set; }

    public int? EvolvesFromId { get; set; }

    public IEnumerable<int>? EvolvesToId { get; set; }

    public IEnumerable<InType> Types { get; set; }

    public IEnumerable<InPokemonPokedexDescription> Versions { get; set; }

    public string ImageName { get; set; }
}

public class InPokemonPokedexDescription
{
    public int VersionId { get; set; }

    public string VersionName { get; set; }

    public string Description { get; set; }
}
public class InType
{
    public int Id { get; set; }
    public string Name { get; set; }
}