namespace PokedexEF.Model;

public partial class PokemonSpeciesEF
{
    public int Id { get; set; }

    public string Identifier { get; set; } = null!;

    public int? GenerationId { get; set; }

    public int? EvolvesFromSpeciesId { get; set; }

    public int? EvolutionChainId { get; set; }

    public int Order { get; set; }

    public virtual ICollection<PokemonEF> Pokemons { get; set; } = new List<PokemonEF>();
}
