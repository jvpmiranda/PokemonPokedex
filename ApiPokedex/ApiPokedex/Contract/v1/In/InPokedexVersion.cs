using PokedexModels.Model;

namespace ApiPokedex.Contract.v1.In;

public class InPokedexVersion
{
    public int Id { get; set; }

    public string Name { get; set; }

    public int VersionGroupId { get; set; }
}
