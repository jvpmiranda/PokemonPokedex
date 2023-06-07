namespace ApiPokedex.Contract.Out
{
    public class OutGetBasicInfoPokemon
    {
        public IEnumerable<OutBasicInfoPokemon> Pokemons { get; set; }
    }

    public class OutBasicInfoPokemon
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Height { get; set; }

        public double Weight { get; set; }

        public int? EvolvesFromSpeciesId { get; set; }

        public int GenerationId { get; set; }
    }

}