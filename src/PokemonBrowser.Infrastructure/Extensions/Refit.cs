using Microsoft.Extensions.DependencyInjection;
using PokemonBrowser.Infrastructure.Framework;
using PokemonBrowser.Infrastructure.Gateway;
using Refit;

namespace PokemonBrowser.Infrastructure.Extensions;

public static class Refit
{
    private const string BaseUrl = "https://pokeapi.co/api/v2";

    public static void AddRefitConfiguration(this IServiceCollection services)
    {
        services
            .AddRefitClient<IPokeApi>()
            .ConfigureHttpClient(client =>
            {
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            })
            .AddPolicyHandler(HttpClientInfrastructure.GetRetryPolicy());
    }
}

