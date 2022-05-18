using AutoFixture.Xunit2;
using Moq;
using PokemonBrowser.Application.Queries.GetRandomPokemon;
using PokemonBrowser.Infrastructure.Gateway;
using PokemonBrowser.Infrastructure.Gateway.Responses;
using Xunit;

namespace PokemonBrowser.UnitTests.Application.Queries;

public class GetRandomPokemonHandlerUnitTests
{
    [Theory, AutoData]
    public async void Handler_ShouldEndSuccessfully(GetRandomPokemonQuery query, PokemonResponse result)
    {
        var apiMock = new Mock<IPokeApi>();
        var handler = new GetRandomPokemonHandler(apiMock.Object);

        apiMock.Setup(x => x.GetPokemon(It.IsAny<int>()))
            .ReturnsAsync(result).Verifiable();

        await handler.Handle(query, CancellationToken.None);

        apiMock.Verify(x => x.GetPokemon(It.IsAny<int>()), Times.Once());
    }
}
