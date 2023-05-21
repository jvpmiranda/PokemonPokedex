using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PokedexEF.Model;

[Table("pokemon")]
public partial class PokemonEF
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("identifier")]
    [StringLength(79)]
    [Unicode(false)]
    public string Identifier { get; set; } = null!;

    [Column("speciesId")]
    public int? SpeciesId { get; set; }

    [Column("height")]
    public int Height { get; set; }

    [Column("weight")]
    public int Weight { get; set; }

    [Column("order")]
    public int Order { get; set; }

    [InverseProperty("Pokemon")]
    public virtual ICollection<PokemonTypeEF> PokemonTypes { get; set; } = new List<PokemonTypeEF>();

    [ForeignKey("SpeciesId")]
    [InverseProperty("Pokemons")]
    public virtual PokemonSpecyEF? Species { get; set; }
}
