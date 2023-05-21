using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PokedexEF.Model;

[Table("pokemonSpecies")]
public partial class PokemonSpecyEF
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("identifier")]
    [StringLength(79)]
    [Unicode(false)]
    public string Identifier { get; set; } = null!;

    [Column("generationId")]
    public int? GenerationId { get; set; }

    [Column("evolvesFromSpeciesId")]
    public int? EvolvesFromSpeciesId { get; set; }

    [Column("evolutionChainId")]
    public int? EvolutionChainId { get; set; }

    [Column("order")]
    public int Order { get; set; }

    [InverseProperty("Species")]
    public virtual ICollection<PokemonEF> Pokemons { get; set; } = new List<PokemonEF>();
}
