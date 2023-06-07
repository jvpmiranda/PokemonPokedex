namespace ApiPokedex.Contract.In
{
    public class InPokemon
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Height { get; set; }

        public double Weight { get; set; }

        public int? EvolvesFrom { get; set; }

        public List<int> EvolvesTo { get; set; }

        public List<InAvailableVersions> AvailableVersions { get; set; }
    }

    public class InAvailableVersions
    {
        public int Id { get; set; }

        public int Name { get; set; }
    }
}