using PokemonBrowser.Infrastructure.Gateway.Responses;
using Refit;

namespace PokemonBrowser.Infrastructure.Gateway;

public interface IPokeApi
{
    [Get("/pokemon/{name}")]
    Task<ApiResponse<PokemonResponse>> GetPokemon(string name);

    [Get("/pokemon/{id}")]
    Task<PokemonResponse> GetPokemon(int id);
}
