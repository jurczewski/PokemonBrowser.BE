using System.Text.Json.Serialization;

namespace PokemonBrowser.Infrastructure.Gateway.Responses;
public class PokemonResponse
{
    public int Id { get; set; }
    public Species? Species { get; set; }
    public Sprites? Sprites { get; set; }
}

public record Species(string? Name);

public class Sprites
{
    [JsonPropertyName("front_default")]
    public string? FrontDefault { get; set; }
}
