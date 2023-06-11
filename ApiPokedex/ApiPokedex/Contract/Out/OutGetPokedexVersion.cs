namespace ApiPokedex.Contract.Out;

public class OutGetPokedexVersion
{
    public IEnumerable<OutPokedexVersion> Versions { get; set; }
}

public class OutPokedexVersion
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string VersionName { get; set; }

    public int GenerationNumber { get; set; }
}