using Microsoft.EntityFrameworkCore;
using PokedexEF.Model;

namespace PokedexEF.DataAccess;

public partial class DbPokedexContext : DbContext
{
    public DbPokedexContext(DbContextOptions<DbPokedexContext> options) : base(options) { }

    public virtual DbSet<PokemonEF> Pokemons { get; set; }

    public virtual DbSet<PokemonSpecyEF> PokemonSpecies { get; set; }

    public virtual DbSet<PokemonTypeEF> PokemonTypes { get; set; }

    public virtual DbSet<TypeOfPokemonEF> TypeOfPokemons { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PokemonEF>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Pokemon_ID");

            entity.HasOne(d => d.Species).WithMany(p => p.Pokemons).HasConstraintName("FK_Pokemon_PokemonSpecies_ID");
        });

        modelBuilder.Entity<PokemonSpecyEF>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_pokemonSpecies_ID");
        });

        modelBuilder.Entity<PokemonTypeEF>(entity =>
        {
            entity.HasKey(e => new { e.PokemonId, e.Slot }).HasName("PK_pokemonTypes_id");

            entity.HasOne(d => d.Pokemon).WithMany(p => p.PokemonTypes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_pokemonId_Pokemon_id");

            entity.HasOne(d => d.Type).WithMany(p => p.PokemonTypes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_typeId_typeOfPokemon_id");
        });

        modelBuilder.Entity<TypeOfPokemonEF>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_typeOfPokemon_ID");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
