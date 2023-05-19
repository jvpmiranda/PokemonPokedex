using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using PokedexEF.Model;

namespace PokedexEF.DataAccess;

public partial class DbPokedexContext : DbContext
{
    public DbPokedexContext()
    {
    }

    public DbPokedexContext(DbContextOptions<DbPokedexContext> options)
        : base(options)
    {
    }

    public virtual DbSet<PokemonEF> Pokemons { get; set; }

    public virtual DbSet<PokemonSpeciesEF> PokemonSpecies { get; set; }

    public virtual DbSet<PokemonTypeEF> PokemonTypes { get; set; }

    public virtual DbSet<TypeOfPokemonEF> TypeOfPokemons { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:Pokedex");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PokemonEF>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Pokemon_ID");

            entity.ToTable("pokemon");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Height).HasColumnName("height");
            entity.Property(e => e.Identifier)
                .HasMaxLength(79)
                .IsUnicode(false)
                .HasColumnName("identifier");
            entity.Property(e => e.Order).HasColumnName("order");
            entity.Property(e => e.SpeciesId).HasColumnName("speciesId");
            entity.Property(e => e.Weight).HasColumnName("weight");

            entity.HasOne(d => d.Species).WithMany(p => p.Pokemons)
                .HasForeignKey(d => d.SpeciesId)
                .HasConstraintName("FK_Pokemon_PokemonSpecies_ID");
        });

        modelBuilder.Entity<PokemonSpeciesEF>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_pokemonSpecies_ID");

            entity.ToTable("pokemonSpecies");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.EvolutionChainId).HasColumnName("evolutionChainId");
            entity.Property(e => e.EvolvesFromSpeciesId).HasColumnName("evolvesFromSpeciesId");
            entity.Property(e => e.GenerationId).HasColumnName("generationId");
            entity.Property(e => e.Identifier)
                .HasMaxLength(79)
                .IsUnicode(false)
                .HasColumnName("identifier");
            entity.Property(e => e.Order).HasColumnName("order");
        });

        modelBuilder.Entity<PokemonTypeEF>(entity =>
        {
            entity.HasKey(e => new { e.PokemonId, e.Slot }).HasName("PK_pokemonTypes_id");

            entity.ToTable("pokemonTypes");

            entity.Property(e => e.PokemonId).HasColumnName("pokemonId");
            entity.Property(e => e.Slot).HasColumnName("slot");
            entity.Property(e => e.TypeId).HasColumnName("typeId");

            entity.HasOne(d => d.Pokemon).WithMany(p => p.PokemonTypes)
                .HasForeignKey(d => d.PokemonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_pokemonId_Pokemon_id");

            entity.HasOne(d => d.Type).WithMany(p => p.PokemonTypes)
                .HasForeignKey(d => d.TypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_typeId_typeOfPokemon_id");
        });

        modelBuilder.Entity<TypeOfPokemonEF>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_typeOfPokemon_ID");

            entity.ToTable("typeOfPokemon");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DamageClassId).HasColumnName("damageClassId");
            entity.Property(e => e.GenerationId).HasColumnName("generationId");
            entity.Property(e => e.Identifier)
                .HasMaxLength(79)
                .IsUnicode(false)
                .HasColumnName("identifier");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
