namespace PokedexApiCaller.Contract.v1.Out;

public class OutTypeOfPokemon : ErrorStatus
{
    public int Id { get; set; }

    public string Name { get; set; }
}