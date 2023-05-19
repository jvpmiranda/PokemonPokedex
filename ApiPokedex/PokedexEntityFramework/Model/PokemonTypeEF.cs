namespace PokedexEF.Model;

public partial class PokemonTypeEF
{
    public int PokemonId { get; set; }

    public int TypeId { get; set; }

    public int Slot { get; set; }

    public virtual PokemonEF Pokemon { get; set; } = null!;

    public virtual TypeOfPokemonEF Type { get; set; } = null!;
}
