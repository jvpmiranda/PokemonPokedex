using PokedexModels.Model;
using System;

namespace PokedexDataAccess.Model.MongoDb
{
    internal static class MongoDbMapper
    {

        #region Mapping DAO classes to Model

        internal static PokemonModel ToPokemonModel(PokemonModelMongoDb pokemon)
        {
            var model = new PokemonModel()
            {
                Id = pokemon.Id,
                Name = pokemon.Name,
                Height = pokemon.Height,
                Weight = pokemon.Weight,
                GenerationNumber = pokemon.GenerationNumber,
                ImageName = pokemon.ImageName,
                Types = pokemon.Types.Select(t => ToTypeModel(t)),
                Versions = ToPokemonVersionModelEnumerable(pokemon.Versions)
            };
            if (pokemon.EvolvesFrom.HasValue)
                model.EvolvesFrom = new PokemonModel() { Id = pokemon.EvolvesFrom.Value };
            if (pokemon.EvolvesTo != null && pokemon.EvolvesTo.Any())
                model.EvolvesTo = pokemon.EvolvesTo.Select(e => new PokemonModel() { Id = e });
            return model;
        }

        internal static PokemonModelMongoDb FromPokemonModel(PokemonModel pokemon)
        {
            var model = new PokemonModelMongoDb()
            {
                Id = pokemon.Id,
                Name = pokemon.Name,
                Height = pokemon.Height,
                Weight = pokemon.Weight,
                GenerationNumber = pokemon.GenerationNumber,
                ImageName = pokemon.ImageName,
                EvolvesFrom = pokemon.EvolvesFrom?.Id,
                Types = FromTypeModelEnumerable(pokemon.Types).ToList(),
                Versions = FromPokemonVersionModelEnumerable(pokemon.Versions).ToList()
            };
            if (pokemon.EvolvesTo.Any())
                model.EvolvesTo = pokemon.EvolvesTo.Select(e => e.Id).ToList();

            return model;
        }

        internal static TypeModel ToTypeModel(TypeModelMongoDb type)
        {
            return new TypeModel()
            {
                Id = type.Id,
                Name = type.Name
            };
        }

        internal static TypeModelMongoDb FromTypeModel(TypeModel type)
        {
            return new TypeModelMongoDb()
            {
                Id = type.Id,
                Name = type.Name
            };
        }

        internal static IEnumerable<TypeModelMongoDb> FromTypeModelEnumerable(IEnumerable<TypeModel> types)
        {
            foreach (var type in types)
                yield return FromTypeModel(type);
        }

        internal static IEnumerable<PokemonVersionModel> ToPokemonVersionModelEnumerable(IEnumerable<PokemonVersionModelMongoDb> pokemonVersion)
        {
            foreach (var pok in pokemonVersion)
                yield return new PokemonVersionModel()
                {
                    VersionId = pok.VersionId,
                    VersionName = pok.VersionName,
                    Description = pok.Description
                };
        }

        internal static IEnumerable<PokemonVersionModelMongoDb> FromPokemonVersionModelEnumerable(IEnumerable<PokemonVersionModel> pokemonVersion)
        {
            foreach (var pok in pokemonVersion)
                yield return FromPokemonVersionModel(pok);
        }

        internal static PokemonVersionModel ToPokemonVersionModel(PokemonVersionModelMongoDb pokemonVersion)
        {
            return new PokemonVersionModel()
            {
                VersionId = pokemonVersion.VersionId,
                VersionName = pokemonVersion.VersionName,
                Description = pokemonVersion.Description
                //VersionGroup = new PokemonVersionGroupModel()
                //{
                //    VersionGroupName = pokemonVersion.GroupName,
                //    VersionGroupGenerationNumber = pokemonVersion.GenerationNumber
                //}
            };
        }

        internal static PokemonVersionModelMongoDb FromPokemonVersionModel(PokemonVersionModel pokemonVersion)
        {
            return new PokemonVersionModelMongoDb()
            {
                VersionId = pokemonVersion.VersionId,
                VersionName = pokemonVersion.VersionName,
                Description = pokemonVersion.Description
            };
        }

        internal static PokedexVersionModel ToPokedexVersionModel(PokedexVersionModelMongoDb pokedexVersion)
        {
            return new PokedexVersionModel()
            {
                Id = pokedexVersion.Id,
                Name = pokedexVersion.Name,
                VersionGroup = new PokedexVersionGroupModel()
                {
                    GenerationNumber = pokedexVersion.GenerationNumber,
                    GroupId = pokedexVersion.GroupId,
                    VersionName = pokedexVersion.VersionName
                }
            };
        }

        internal static PokedexVersionModelMongoDb FromPokedexVersionModel(PokedexVersionModel pokedexVersion)
        {
            return new PokedexVersionModelMongoDb()
            {
                Id = pokedexVersion.Id,
                Name = pokedexVersion.Name,
                GenerationNumber = pokedexVersion.VersionGroup.GenerationNumber,
                GroupId = pokedexVersion.VersionGroup.GroupId,
                VersionName = pokedexVersion.VersionGroup.VersionName
            };
        }


        internal static PokedexVersionGroupModel ToPokedexVersionGroupModel(PokedexVersionGroupModelMongoDb pokedexVersion)
        {
            return new PokedexVersionGroupModel()
            {
                GenerationNumber = pokedexVersion.GenerationNumber,
                GroupId = pokedexVersion.GroupId,
                VersionName = pokedexVersion.VersionName
            };
        }

        internal static PokedexVersionGroupModelMongoDb FromPokedexVersionGroupModel(PokedexVersionGroupModel pokedexVersion)
        {
            return new PokedexVersionGroupModelMongoDb()
            {
                GenerationNumber = pokedexVersion.GenerationNumber,
                GroupId = pokedexVersion.GroupId,
                VersionName = pokedexVersion.VersionName
            };
        }

        internal static ImageModel ToImageModel(ImageModelMongoDb image)
        {
            return new ImageModel()
            {
                PokemonId = image.Id,
                Image = image.Image
            };
        }

        internal static ImageModelMongoDb FromImageModel(ImageModel image)
        {
            return new ImageModelMongoDb()
            {
                Id = image.PokemonId,
                Image = image.Image
            };
        }

        #endregion  Mapping DAO classes to Model
    }
}
