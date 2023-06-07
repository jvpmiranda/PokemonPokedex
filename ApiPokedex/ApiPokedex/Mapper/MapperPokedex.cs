using ApiPokedex.Contract.In;
using ApiPokedex.Contract.Out;
using PokedexServices.Model;

namespace ApiPokedex.Mapper
{
    public static class MapperPokedex
    {

        public static PokemonModel ConvertToModel(InPokemon pokemon)
        {
            PokemonModel pok = new PokemonModel()
            {
                Id = pokemon.Id,
                Identifier = pokemon.Name
            };
            return pok;
        }

        public static IEnumerable<OutBasicInfoPokemon> ConvertToEnumerableContract(IEnumerable<PokemonModel> pokemons)
        {
            foreach (var item in pokemons)
                yield return ConvertToContract(item);
        }

        public static OutBasicInfoPokemon ConvertToContract(PokemonModel pokemon)
        {
            OutBasicInfoPokemon pok = new OutBasicInfoPokemon()
            {
                Id = pokemon.Id,
                Name = pokemon.Identifier,
                Height = pokemon.Height,
                Weight = pokemon.Weight,
                EvolvesFromSpeciesId = pokemon.EvolvesFrom,
                GenerationId = pokemon.GenerationId
            };
            return pok;
        }
    }
}
