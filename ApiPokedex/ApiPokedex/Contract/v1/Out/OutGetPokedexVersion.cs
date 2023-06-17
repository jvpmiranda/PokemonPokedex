namespace ApiPokedex.Contract.v1.Out;

public class OutGetPokedexVersion : List<OutPokedexVersion> { }

public class OutPokedexVersion
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string VersionName { get; set; }

    public int GenerationNumber { get; set; }
}