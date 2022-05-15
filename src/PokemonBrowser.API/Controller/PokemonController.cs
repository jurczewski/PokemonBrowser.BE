using System.Net;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using PokemonBrowser.Application.Queries.GetPokemonByName;
using PokemonBrowser.Application.Queries.GetRandomPokemon;

namespace PokemonBrowser.API.Controller;

public class PokemonController
{
    private readonly ILogger<PokemonController> _logger;
    private readonly IMediator _mediator;

    public PokemonController(ILogger<PokemonController> log, IMediator mediator)
    {
        _logger = log;
        _mediator = mediator;
    }

    [FunctionName("getByName")]
    [OpenApiSecurity("code", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
    [OpenApiOperation(operationId: "getByName", tags: new[] { "name" })]
    [OpenApiParameter(name: "name", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "The pokemon **Name** parameter")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(GetPokemonByNameQueryResult), Description = "The OK response")]
    public async Task<ActionResult<GetPokemonByNameQueryResult>> Get([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req)
    {
        _logger.LogInformation("Get pokemon by name");
        string name = req.Query["name"];

        var pokemon = await _mediator.Send(new GetPokemonByNameQuery(name));
        return new OkObjectResult(pokemon);
    }

    [FunctionName("getRandom")]
    [OpenApiSecurity("code", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
    [OpenApiOperation(operationId: "getRandom", tags: new[] { "random" })]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(GetRandomPokemonQueryResult), Description = "The OK response")]
    public async Task<ActionResult<GetRandomPokemonQueryResult>> GetRandom([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req)
    {
        _logger.LogInformation("Get random pokemon");

        var pokemon = await _mediator.Send(new GetRandomPokemonQuery());
        return new OkObjectResult(pokemon);
    }
}
