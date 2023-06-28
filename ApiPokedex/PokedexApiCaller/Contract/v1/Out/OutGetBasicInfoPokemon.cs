namespace PokedexApiCaller.Contract.v1.Out;

public class OutGetBasicInfoPokemon : List<OutBasicInfoPokemon> { }

public class OutBasicInfoPokemon : ErrorStatus
{
    public int Id { get; set; }

    public string Name { get; set; }

    public int GenerationNumber { get; set; }

    public string ImageName { get; set; }
}