using MediatR;
using PokemonBrowser.Infrastructure.Gateway;

namespace PokemonBrowser.Application.Queries.GetRandomPokemon;

public class GetRandomPokemonHandler : IRequestHandler<GetRandomPokemonQuery, GetRandomPokemonQueryResult>
{
    private readonly IPokeApi _pokeApi;
    private const int MaxId = 899; // 898 is the last pokemon id

    public GetRandomPokemonHandler(IPokeApi pokeApi)
    {
        _pokeApi = pokeApi;
    }

    public async Task<GetRandomPokemonQueryResult> Handle(GetRandomPokemonQuery request, CancellationToken cancellationToken)
    {
        var r = new Random();
        var randomId = r.Next(1, MaxId);

        var pokemonResponse = await _pokeApi.GetPokemon(randomId);

        return new GetRandomPokemonQueryResult
        {
            Id = pokemonResponse.Id,
            Name = pokemonResponse.Species?.Name,
            FrontImageUrl = pokemonResponse.Sprites?.FrontDefault
        };
    }
}
