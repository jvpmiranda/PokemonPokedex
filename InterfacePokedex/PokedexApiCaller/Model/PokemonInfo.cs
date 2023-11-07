using PokedexApiCaller.Contract.v1.Out;

namespace PokedexApiCaller.Model;

public class PokemonInfo
{
    public int Id { get; set; }

    public string Name { get; set; }

    public int Height { get; set; }

    public double Weight { get; set; }

    public int GenerationNumber { get; set; }

    public List<PokemonDescription> Versions { get; set; } = new List<PokemonDescription>();

    public List<PokemonTypes> Types { get; set; } = new List<PokemonTypes>();

    public string ImageName { get; set; }

    public List<PokemonInfo> EvolvesTo { get; set; } = new List<PokemonInfo>();

    public PokemonInfo EvolvesFrom { get; set; }

    public static implicit operator PokemonInfo(OutPokemon pok)
    {
        PokemonInfo outPokemon = (OutPokemonInfo)pok;

        if (pok.EvolvesFrom != null)
            outPokemon.EvolvesFrom = pok.EvolvesFrom;

        if (pok.EvolvesTo != null)
            pok.EvolvesTo.ForEach(p => outPokemon.EvolvesTo.Add(p));

        return outPokemon;
    }

    public static implicit operator PokemonInfo(OutPokemonInfo pok)
    {
        var outPokemon = new PokemonInfo();
        outPokemon.Id = pok.Id;
        outPokemon.Name = pok.Name;
        outPokemon.Height = pok.Height;
        outPokemon.Weight = pok.Weight;
        outPokemon.GenerationNumber = pok.GenerationNumber;
        outPokemon.ImageName = pok.ImageName;

        outPokemon.Types = new List<PokemonTypes>();
        pok.Types.ForEach(t => outPokemon.Types.Add(t));

        outPokemon.Versions = new List<PokemonDescription>();
        pok.Versions.ForEach(v => outPokemon.Versions.Add(v));

        return outPokemon;
    }
}