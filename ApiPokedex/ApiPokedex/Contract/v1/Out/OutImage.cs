namespace ApiPokedex.Contract.v1.Out;

public class OutImage : ErrorStatus
{
    public int PokemonId { get; set; }

    public byte[] Image { get; set; }
}