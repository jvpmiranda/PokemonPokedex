namespace ApiPokedex.Contract.v1.In;

public class InPokedexVersion
{
    public int Id { get; set; }

    public string Name { get; set; }

    public int GroupId { get; set; }

    public string VersionName { get; set; }

    public int GenerationNumber { get; set; }
}
