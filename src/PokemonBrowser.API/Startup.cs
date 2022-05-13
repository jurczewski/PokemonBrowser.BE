using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using PokemonBrowser.API;
using PokemonBrowser.API.Extensions;
using PokemonBrowser.Application;

[assembly: FunctionsStartup(typeof(Startup))]
namespace PokemonBrowser.API;

public class Startup : FunctionsStartup
{
    public override void Configure(IFunctionsHostBuilder builder)
    {
        builder.Services.AddApplication();
        builder.Services.AddRefitConfiguration();
    }
}
