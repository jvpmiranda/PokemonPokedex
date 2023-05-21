using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PokedexEF.Model;

[Table("typeOfPokemon")]
public partial class TypeOfPokemonEF
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("identifier")]
    [StringLength(79)]
    [Unicode(false)]
    public string Identifier { get; set; } = null!;

    [Column("generationId")]
    public int GenerationId { get; set; }

    [Column("damageClassId")]
    public int? DamageClassId { get; set; }

    [InverseProperty("Type")]
    public virtual ICollection<PokemonTypeEF> PokemonTypes { get; set; } = new List<PokemonTypeEF>();
}
