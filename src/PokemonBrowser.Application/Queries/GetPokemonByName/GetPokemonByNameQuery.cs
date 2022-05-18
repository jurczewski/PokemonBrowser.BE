using MediatR;

namespace PokemonBrowser.Application.Queries.GetPokemonByName;

public record GetPokemonByNameQuery(string Name) : IRequest<GetPokemonByNameQueryResult>;
