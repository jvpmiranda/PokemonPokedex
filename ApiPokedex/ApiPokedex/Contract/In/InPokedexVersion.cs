using PokedexModels.Model;

namespace ApiPokedex.Contract.In;

public class InPokedexVersion
{
    public int Id { get; set; }

    public string Name { get; set; }

    public int VersionGroupId { get; set; }
}
