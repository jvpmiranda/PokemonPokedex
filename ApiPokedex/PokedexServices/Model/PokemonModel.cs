namespace PokedexServices.Model
{
    public class PokemonModel
    {
        public int Id { get; set; }

        public string Identifier { get; set; } = null!;

        public int Height { get; set; }

        public int Weight { get; set; }

        public ICollection<PokemonTypeModel> Types { get; set; } = null!;
    }
}