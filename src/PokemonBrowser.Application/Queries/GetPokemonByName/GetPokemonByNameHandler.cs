using MediatR;
using PokemonBrowser.Infrastructure.Gateway;

namespace PokemonBrowser.Application.Queries.GetPokemonByName;

public class GetPokemonByNameHandler : IRequestHandler<GetPokemonByNameQuery, GetPokemonByNameQueryResult>
{
    private readonly IPokeApi _pokeApi;

    public GetPokemonByNameHandler(IPokeApi pokeApi)
    {
        _pokeApi = pokeApi;
    }

    public async Task<GetPokemonByNameQueryResult> Handle(GetPokemonByNameQuery request, CancellationToken cancellationToken)
    {
        var pokemonResponse = await _pokeApi.GetPokemon(request.Name);
        return new GetPokemonByNameQueryResult
        {
            Id = pokemonResponse.Id,
            Name = pokemonResponse.Species?.Name,
            FrontImageUrl = pokemonResponse.Sprites?.FrontDefault
        };
    }
}
