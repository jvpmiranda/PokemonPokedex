using DatabaseUtilitary.AppSettings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PokedexApiCaller;
using PokedexApiCaller.Caller;

namespace DatabaseUtilitary;

internal class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Api Caller!");

        var configuration = StartConfiguration();
        var build = StartServices();
        var jwtSettings = ActivatorUtilities.CreateInstance<JwtSettings>(build.Services);
        var apiUrl = ActivatorUtilities.CreateInstance<ApiUrl>(build.Services);
        ConfigurationBinder.Bind(configuration, "JwtSettings", jwtSettings);
        ConfigurationBinder.Bind(configuration, "ApiUrl", apiUrl);

        var auth = new AuthCallerApiCaller(apiUrl.PokedexSQLServer, jwtSettings.Secret);
        var pok = new PokedexVersionApiCaller(apiUrl.PokedexSQLServer);

        pok.Auth = auth.GetToken("Joao").Result;
        var versions = pok.GetVersion(null).Result;

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

    static IHost StartServices()
    {
        var host = Host.CreateDefaultBuilder();
        host.ConfigureServices((context, services) =>
            {
                services.AddSingleton<ApiUrl>();
            });
        return host.Build();
    }
}