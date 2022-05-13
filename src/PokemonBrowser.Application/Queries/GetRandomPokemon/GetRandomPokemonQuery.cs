using MediatR;

namespace PokemonBrowser.Application.Queries.GetRandomPokemon;

public record GetRandomPokemonQuery : IRequest<GetRandomPokemonQueryResult>;

