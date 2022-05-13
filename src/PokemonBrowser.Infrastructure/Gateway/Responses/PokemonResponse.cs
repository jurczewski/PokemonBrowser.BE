using Refit;

namespace PokemonBrowser.Infrastructure.Gateway.Responses;
public class PokemonResponse
{
    public int Id { get; set; }
    public Species? Species { get; set; }
    public Sprites? Sprites { get; set; }
}

public record Species(string? Name);

public record Sprites([AliasAs("front_default")] string? front_default);
