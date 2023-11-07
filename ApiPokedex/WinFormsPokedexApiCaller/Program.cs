using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PokedexApiCaller.Config;
using PokedexApiCaller.Factory;
using PokedexApiCaller.Interfaces;
using PokedexApiCaller.Services;
using WinFormsPokedexApiCaller.UI;

namespace WinFormsPokedexApiCaller;

internal static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.
        ApplicationConfiguration.Initialize();

        var host = StartServices();

        Application.Run(host.Services.GetRequiredService<Main>());
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
                services.AddTransient<Main>();
            });
        return host.Build();
    }
}