using DatabaseUtilitary.AppSettings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PokedexApiCaller.Interfaces;
using PokedexApiCaller.Services;

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
        var buildSqlServer = StartServicesSqlServer(jwtSettings, apiUrl);

        var auth = buildSqlServer.Services.GetRequiredService<IAuthCallerApiCaller>();
        var vers = buildSqlServer.Services.GetRequiredService<IPokedexVersionApiCaller>();
        var img = buildSqlServer.Services.GetRequiredService<IPokemonImageApiCaller>();
        var pok = buildSqlServer.Services.GetRequiredService<IPokemonApiCaller>();

        pok.Auth = auth.GetToken("Joao").Result;
        var versions = vers.GetVersion(null).Result;

        Parallel.ForEach(versions, item => 
            Console.WriteLine(item.Id.ToString() + " - " + item.Name)
        );

        Console.ReadKey();
    }

    static IConfigurationRoot StartConfiguration()
    {
        var config = new ConfigurationBuilder();
        config.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
        return config.Build();
    }

    static IHost StartServicesSqlServer(JwtSettings jwtSettings, ApiUrl apiUrl)
    {
        var host = Host.CreateDefaultBuilder();
        host.ConfigureServices((context, services) =>
        {
            //services.AddSingleton<ApiUrl>();
            //services.AddSingleton<JwtSettings>();
            services.AddTransient<IPokemonImageApiCaller>(x => new PokemonImageApiCaller(apiUrl.PokedexSQLServer));
            services.AddTransient<IPokemonApiCaller>(x => new PokemonApiCaller(apiUrl.PokedexSQLServer));
            services.AddTransient<IPokedexVersionApiCaller>(x => new PokedexVersionApiCaller(apiUrl.PokedexSQLServer));
            services.AddTransient<IAuthCallerApiCaller>(x => new AuthCallerApiCaller(apiUrl.PokedexSQLServer, jwtSettings.Secret));
        });
        return host.Build();
    }
}