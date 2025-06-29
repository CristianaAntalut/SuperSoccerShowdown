using Amazon.Lambda.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SuperSoccerShowdown.Common.Dtos;
using SuperSoccerShowdown.TeamGenerator.Models;
using SuperSoccerShowdown.TeamGenerator.Interfaces;
using SuperSoccerShowdown.TeamGenerator.Strategies;


// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace TeamGenerator;

public partial class TeamGeneratorFunction
{
    private ServiceCollection _serviceCollection;
    private readonly ILogger _logger;

    public TeamGeneratorFunction()
    {
        using var loggerFactory = LoggerFactory.Create(builder =>
        {
            builder
                .SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Information)
                .AddConsole()
                .AddDebug();
        });

        _logger = loggerFactory.CreateLogger(nameof(TeamGeneratorFunction));
        ConfigureServices();
    }


    public TeamGeneratorResponse FunctionHandler(TeamGeneratorRequest request, ILambdaContext context)
    {
        using ServiceProvider serviceProvider = _serviceCollection.BuildServiceProvider();
        var teamContext = serviceProvider.GetRequiredService<ITeamContext>();

        _logger.LogInformation($"Generating team with strategyService: {request.StrategyType} with number of offence players: {request.numberOfOffencePlayers}");
        var result = teamContext.GenerateTeam(request.StrategyType, request.Players, request.numberOfOffencePlayers);

        return new TeamGeneratorResponse
        {
            Players = result
        };
    }

    private void ConfigureServices()
    {
        _serviceCollection = new ServiceCollection();

        _serviceCollection.AddLogging();

        _serviceCollection.AddScoped<ITeamStrategy, WeightFirstStrategy>();
        _serviceCollection.AddScoped<ITeamStrategy, HeightFirstStrategy>();
        _serviceCollection.AddScoped<IStrategyFactory, StrategyFactory>();

        _serviceCollection.AddScoped<ITeamContext, TeamContext>();

        FixHttpClientError();
    }

    private void FixHttpClientError()
    {
        //https://github.com/openai/openai-dotnet/issues/297
        var metricsFilterDescriptor = _serviceCollection.FirstOrDefault(descriptor =>
                    descriptor.ImplementationType?.ToString() == "Microsoft.Extensions.Http.MetricsFactoryHttpMessageHandlerFilter");
        _serviceCollection.Remove(metricsFilterDescriptor);
    }
}
