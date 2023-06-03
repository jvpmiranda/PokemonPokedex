using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace ApiPokedex.Contract
{
    public class PokemonV2
    {
        public int PokemonId { get; set; }

        public string PokemonName { get; set; }

        public int Height { get; set; }

        public double Weight { get; set; }

        public ICollection<PokemonTypeV2> Types { get; set; }

        public int? EvolveFrom { get; set; }

        public List<int> EvolveTo { get; set; }

        public string Description { get; set; }
    }

    public class PokemonTypeV2
    {
        public int TypeId { get; set; }

        public string TypeName { get; set; }
    }
}