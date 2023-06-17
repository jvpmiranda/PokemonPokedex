using System.Data.Common;

namespace PokedexModels.Model;

public class PokemonModelFull : PokemonModel
{
    public PokemonModelFull(PokemonModel pokemon)
    {
        GenerationNumber = pokemon.GenerationNumber;
        Name = pokemon.Name;
        Description = pokemon.Description;
        Weight = pokemon.Weight;
        Height = pokemon.Height;
        Id = pokemon.Id;
        EvolvesFrom = pokemon.EvolvesFrom;
        EvolvesTo = pokemon.EvolvesTo;
        Types = pokemon.Types;
        Evolutions = new List<PokemonModelFull>();
    }

    public PokemonModelFull PreEvolution { get; set; }
    public List<PokemonModelFull> Evolutions { get; set; }
}
