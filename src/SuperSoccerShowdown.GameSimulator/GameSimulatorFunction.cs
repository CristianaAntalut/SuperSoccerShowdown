using Amazon.Lambda.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SuperSoccerShowdown.Common.Dtos;
using SuperSoccerShowdown.Common.Models.GameSimulator;
using SuperSoccerShowdown.GameSimulator;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace GameSimulator;

public class GameSimulatorFunction
{
    private ServiceCollection _serviceCollection;

    public GameSimulatorFunction()
    {
        ConfigureServices();
    }

    public GameSimulatorResponse FunctionHandler(GameSimulatorRequest request, ILambdaContext context)
    {
        var serviceProvider = _serviceCollection.BuildServiceProvider();
        var gameSimulator = serviceProvider.GetRequiredService<IGameService>();

        var result = gameSimulator.Play(request.FirstTeam, request.SecondTeam);

        return new GameSimulatorResponse
        {
            GameHighlights = result
        };
    }
  
    private void ConfigureServices()
    {
        _serviceCollection = new ServiceCollection();

        _serviceCollection.AddLogging();
        var config = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.json")
           .Build();
        _serviceCollection.Configure<GameConfig>(config.GetSection("GameConfig"));

        _serviceCollection.AddTransient<IGameService, GameService>();
    }
}
