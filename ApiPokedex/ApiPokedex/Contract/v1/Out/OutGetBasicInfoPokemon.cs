namespace ApiPokedex.Contract.v1.Out;

public class OutGetBasicInfoPokemon : List<OutBasicInfoPokemon> { }

public class OutBasicInfoPokemon
{
    public int Id { get; set; }

    public string Name { get; set; }

    public int Height { get; set; }

    public double Weight { get; set; }

    public int? EvolvesFrom { get; set; }

    public int GenerationNumber { get; set; }
}