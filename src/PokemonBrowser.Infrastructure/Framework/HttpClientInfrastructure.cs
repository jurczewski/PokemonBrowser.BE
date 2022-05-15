using System.Net;
using Polly;
using Polly.Extensions.Http;

namespace PokemonBrowser.Infrastructure.Framework;

public class HttpClientInfrastructure
{
    public static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
    {
        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .OrResult(msg => msg.StatusCode == HttpStatusCode.NotFound)
            .WaitAndRetryAsync(1, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

    }
}
