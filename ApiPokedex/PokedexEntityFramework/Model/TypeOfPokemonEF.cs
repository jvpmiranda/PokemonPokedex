namespace PokedexEF.Model;

public partial class TypeOfPokemonEF
{
    public int Id { get; set; }

    public string Identifier { get; set; } = null!;

    public int GenerationId { get; set; }

    public int? DamageClassId { get; set; }

    public virtual ICollection<PokemonTypeEF> PokemonTypes { get; set; } = new List<PokemonTypeEF>();
}
