using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PokedexApiCaller.Config;
using PokedexApiCaller.Contract.v1.In;
using PokedexApiCaller.Contract.v1.Out;
using PokedexApiCaller.Factory;
using PokedexApiCaller.Interfaces;
using PokedexApiCaller.Services;

namespace DatabaseUtilitary;

internal class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Api Caller!");

        var host = StartServices();
        var buildDest = StartServices();

        var deuBom1 = MigrateImage(buildDest).Result;
        //var deuBom2 = MigratePokedexVersions(buildOrig, buildDest).Result;
        //var deuBom3 = MigratePokemon(buildOrig, buildDest).Result;

        Console.ReadKey();
    }

    static async Task<bool> MigrateImage(IHost buildOrig)
    {

        var authOrig = buildOrig.Services.GetRequiredService<IAuthApiCaller>();
        var pokemonOrig = buildOrig.Services.GetRequiredService<IPokemonApiCaller>();
        var imageOrig = buildOrig.Services.GetRequiredService<IPokemonImageApiCaller>();

        pokemonOrig.Auth = await authOrig.GetToken("Joao");
        var pokemons = await pokemonOrig.GetBasicInfo(1);

        Parallel.ForEach(pokemons, pok =>
        {
            imageOrig.Auth = authOrig.GetToken("Joao").Result;

            Console.WriteLine("Pokemon: " + pok.Name);
            var img = imageOrig.GetImage(pok.Id).Result;

        });

        return true;
    }

    static async Task<bool> MigratePokedexVersions(IHost buildOrig, IHost buildDest)
    {

        var authOrig = buildOrig.Services.GetRequiredService<IAuthApiCaller>();
        var pokedexVersionsOrig = buildOrig.Services.GetRequiredService<IPokedexVersionApiCaller>();

        var authDest = buildDest.Services.GetRequiredService<IAuthApiCaller>();
        var pokedexVersionsDest = buildDest.Services.GetRequiredService<IPokedexVersionApiCaller>();

        pokedexVersionsOrig.Auth = await authOrig.GetToken("Joao");
        pokedexVersionsDest.Auth = await authDest.GetToken("Joao");
        var versions = await  pokedexVersionsOrig.GetVersion(1);

        Parallel.ForEach(versions, ver =>
        {
            Console.WriteLine("Version: " + ver.Name);
            pokedexVersionsDest.Post(ConvertToInPokedexVersion(ver));

        });

        return true;
    }

    static async Task<bool> MigratePokemon(IHost buildOrig, IHost buildDest)
    {

        var authOrig = buildOrig.Services.GetRequiredService<IAuthApiCaller>();
        var pokemonOrig = buildOrig.Services.GetRequiredService<IPokemonApiCaller>();

        var authDest = buildDest.Services.GetRequiredService<IAuthApiCaller>();
        var pokemonDest = buildDest.Services.GetRequiredService<IPokemonApiCaller>();

        pokemonOrig.Auth = authOrig.GetToken("Joao").Result;
        var pokemons = pokemonOrig.GetBasicInfo(0).Result;
        pokemonDest.Auth = authDest.GetToken("Joao").Result;

        Parallel.ForEach(pokemons, pok =>
        {
            pokemonOrig.Auth = authOrig.GetToken("Joao").Result;
            pokemonDest.Auth = authDest.GetToken("Joao").Result;

            Console.WriteLine("Pokemon: " + pok.Name);
            var img = pokemonOrig.GetInfo(pok.Id, 0).Result;
            pokemonDest.Post(ConvertToInPokemon(img));
        }
        );

        return true;
    }

    static IHost StartServices()
    {
        var host = Host.CreateDefaultBuilder()
            .ConfigureServices((builder, services) =>
            {
                services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
                services.Configure<PokedexSettings>(builder.Configuration.GetSection("PokedexSettings"));
                services.AddTransient<IPokemonImageApiCaller, PokemonImageApiCaller>();
                services.AddTransient<IPokemonApiCaller, PokemonApiCaller>();
                services.AddTransient<IPokedexVersionApiCaller, PokedexVersionApiCaller>();
                services.AddScoped<IAuthApiCaller, AuthApiCaller>();
                services.AddSingleton<FactoryHttpClient>();
            });
        return host.Build();
    }

    private static InImage ConvertToInImage(OutImage img)
    {
        return new InImage() { PokemonId = img.PokemonId, Image = img.Image };
    }

    private static InPokedexVersion ConvertToInPokedexVersion(OutPokedexVersion pokedexVersion)
    {
        return new InPokedexVersion()
        {
            Id = pokedexVersion.Id,
            GenerationNumber = pokedexVersion.GenerationNumber,
            GroupId = pokedexVersion.GroupId,
            Name = pokedexVersion.Name,
            VersionName = pokedexVersion.VersionName
        };
    }

    private static InPokemon ConvertToInPokemon(OutPokemon pokInfo)
    {
        return new InPokemon()
        {
            Id = pokInfo.Id,
            GenerationNumber = pokInfo.GenerationNumber,
            Height = pokInfo.Height,
            Weight = pokInfo.Weight,
            Name = pokInfo.Name,
            ImageName = pokInfo.ImageName,
            Types = pokInfo.Types.Select(e => new InType() { Id = e.Id, Name = e.Name }),
            EvolvesToId = pokInfo.EvolvesTo.Select(e => e.Id),
            EvolvesFromId = pokInfo.EvolvesFrom?.Id,
            Versions = pokInfo.Versions.Select(v =>
                new InPokemonPokedexDescription()
                {
                    VersionId = v.VersionId,
                    Description = v.Description,
                    VersionName = v.VersionName
                })
        };
    }
}