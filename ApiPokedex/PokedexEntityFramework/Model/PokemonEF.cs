namespace PokedexEF.Model;

public partial class PokemonEF
{
    public int Id { get; set; }

    public string Identifier { get; set; } = null!;

    public int? SpeciesId { get; set; }

    public int Height { get; set; }

    public int Weight { get; set; }

    public int Order { get; set; }

    public virtual ICollection<PokemonTypeEF> PokemonTypes { get; set; } = new List<PokemonTypeEF>();

    public virtual PokemonSpeciesEF? Species { get; set; }
}
