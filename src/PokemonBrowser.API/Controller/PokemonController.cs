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
    [OpenApiOperation(operationId: "Run", tags: new[] { "name" })]
    [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
    [OpenApiParameter(name: "name", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "The pokemon **Name** parameter")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
    public async Task<ActionResult<GetPokemonByNameQueryResult>> Get([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request.");
        string name = req.Query["name"];

        var pokemon = await _mediator.Send(new GetPokemonByNameQuery(name));
        return new OkObjectResult(pokemon);
    }
}
