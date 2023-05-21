namespace ApiPokedex.Contract
{
    public class PokemonV2
    {
        public int PokemonId { get; set; }

        public string PokemonName { get; set; }

        public int Height { get; set; }

        public int Weight { get; set; }
        
        public ICollection<PokemonTypeV2> Types { get; set; }
    }

    public class PokemonTypeV2
    {
        public int TypeId { get; set; }

        public string TypeName { get; set; }
    }
}