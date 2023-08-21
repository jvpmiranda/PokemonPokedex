using DatabaseUtilitary.AppSettings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PokedexApiCaller.Contract.v1.In;
using PokedexApiCaller.Contract.v1.Out;
using PokedexApiCaller.Interfaces;
using PokedexApiCaller.Services;
using System.Text;

namespace DatabaseUtilitary;

internal class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Api Caller!");

        var configuration = StartConfiguration();
        JwtSettings jwtSettings = new();
        ApiUrl apiUrl = new();
        ConfigurationBinder.Bind(configuration, "JwtSettings", jwtSettings);
        ConfigurationBinder.Bind(configuration, "ApiUrl", apiUrl);

        //var buildOrig = StartServices(jwtSettings, apiUrl.PokedexMongoDb);
        //var buildDest = StartServices(jwtSettings, apiUrl.PokedexSqlServer);

        //var deuBom1 = MigrateImage(buildOrig, buildDest).Result;
        //var deuBom2 = MigratePokedexVersions(buildOrig, buildDest).Result;
        //var deuBom3 = MigratePokemon(buildOrig, buildDest).Result;

        Console.ReadKey();
    }

    static async Task<bool> MigrateImage(IHost buildOrig, IHost buildDest)
    {

        var authOrig = buildOrig.Services.GetRequiredService<IAuthApiCaller>();
        var pokemonOrig = buildOrig.Services.GetRequiredService<IPokemonApiCaller>();
        var imageOrig = buildOrig.Services.GetRequiredService<IPokemonImageApiCaller>();

        var authDest = buildDest.Services.GetRequiredService<IAuthApiCaller>();
        var imageDest = buildDest.Services.GetRequiredService<IPokemonImageApiCaller>();

        pokemonOrig.Auth = await authOrig.GetToken("Joao");
        var pokemons = await pokemonOrig.GetBasicInfo(0);
        imageDest.Auth = await authDest.GetToken("Joao");

        Parallel.ForEach(pokemons, pok =>
        {
            imageOrig.Auth = authOrig.GetToken("Joao").Result;
            imageDest.Auth = authDest.GetToken("Joao").Result;

            Console.WriteLine("Pokemon: " + pok.Name);
            var img = imageOrig.GetImage(pok.Id).Result;
            imageDest.Post(ConvertToInImage(img));

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

    static IConfigurationRoot StartConfiguration()
    {
        var config = new ConfigurationBuilder();
        config.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
        return config.Build();
    }

    static IHost StartServices(JwtSettings jwtSettings, string url)
    {
        var host = Host.CreateDefaultBuilder();
        host.ConfigureServices((context, services) =>
        {
            services.AddTransient<IPokemonImageApiCaller>(x => new PokemonImageApiCaller(url));
            services.AddTransient<IPokemonApiCaller>(x => new PokemonApiCaller(url));
            services.AddTransient<IPokedexVersionApiCaller>(x => new PokedexVersionApiCaller(url));
            services.AddScoped<IAuthApiCaller>(x => new AuthApiCaller(url, jwtSettings.Secret));
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
                new InPokemonVersion()
                {
                    VersionId = v.VersionId,
                    Description = v.Description,
                    VersionName = v.VersionName
                })
        };
    }
}