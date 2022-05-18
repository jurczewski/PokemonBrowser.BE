using System.Net;
using AutoFixture.Xunit2;
using Moq;
using PokemonBrowser.Application.Queries.GetPokemonByName;
using PokemonBrowser.Infrastructure.Gateway;
using PokemonBrowser.Infrastructure.Gateway.Responses;
using Refit;
using Xunit;

namespace PokemonBrowser.UnitTests.Application.Queries;

public class GetPokemonByNameHandlerUnitTests
{
    [Theory, AutoData]
    public async void Handler_ShouldEndSuccessfully(GetPokemonByNameQuery query, PokemonResponse result)
    {
        var apiMock = new Mock<IPokeApi>();
        var handler = new GetPokemonByNameHandler(apiMock.Object);

        var response = new ApiResponse<PokemonResponse>(new HttpResponseMessage(HttpStatusCode.OK), result, new RefitSettings());

        apiMock.Setup(x => x.GetPokemon(It.IsAny<string>()))
            .ReturnsAsync(response).Verifiable();

        await handler.Handle(query, CancellationToken.None);

        apiMock.Verify(x => x.GetPokemon(It.IsAny<string>()), Times.Once());
    }
}
