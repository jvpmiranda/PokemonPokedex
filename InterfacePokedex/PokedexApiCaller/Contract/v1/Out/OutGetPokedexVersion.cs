namespace PokedexApiCaller.Contract.v1.Out;

public class OutGetPokedexVersion : List<OutPokedexVersion> { }

public class OutPokedexVersion : ErrorStatus
{
    public int Id { get; set; }

    public string Name { get; set; }

    public int GroupId { get; set; }

    public string VersionName { get; set; }

    public int GenerationNumber { get; set; }
}