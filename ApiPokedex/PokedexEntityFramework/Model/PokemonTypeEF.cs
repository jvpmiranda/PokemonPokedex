using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PokedexEF.Model;

[PrimaryKey("PokemonId", "Slot")]
[Table("pokemonTypes")]
public partial class PokemonTypeEF
{
    [Key]
    [Column("pokemonId")]
    public int PokemonId { get; set; }

    [Column("typeId")]
    public int TypeId { get; set; }

    [Key]
    [Column("slot")]
    public int Slot { get; set; }

    [ForeignKey("PokemonId")]
    [InverseProperty("PokemonTypes")]
    public virtual PokemonEF Pokemon { get; set; } = null!;

    [ForeignKey("TypeId")]
    [InverseProperty("PokemonTypes")]
    public virtual TypeOfPokemonEF Type { get; set; } = null!;
}
