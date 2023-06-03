namespace PokedexServices.Model
{
    public class PokemonModel
    {
        public PokemonModel()
        {
            Types = new List<PokemonTypeModel>();
            Description = new List<PokemonDescription>();
        }
        public int Id { get; set; }

        public string Identifier { get; set; } = null!;

        public int Height { get; set; }

        public double Weight { get; set; }

        public ICollection<PokemonTypeModel> Types { get; set; }

        public ICollection<PokemonDescription> Description { get; set; }
    }
}