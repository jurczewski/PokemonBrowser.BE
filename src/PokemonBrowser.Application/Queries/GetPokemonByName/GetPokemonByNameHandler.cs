using MediatR;
using PokemonBrowser.Infrastructure.Gateway;
using PokemonBrowser.Infrastructure.Gateway.Responses;

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
        var response = await _pokeApi.GetPokemon(request.Name.ToLowerInvariant());

        if (!response.IsSuccessStatusCode)
        {
            return new GetPokemonByNameQueryResult();
        }

        var pokemon = response.Content ?? new PokemonResponse();

        return new GetPokemonByNameQueryResult
        {
            Id = pokemon.Id,
            Name = pokemon.Species?.Name,
            FrontImageUrl = pokemon.Sprites?.FrontDefault
        };
    }
}
