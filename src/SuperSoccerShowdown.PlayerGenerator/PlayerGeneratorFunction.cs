using Amazon.Lambda.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PokeApiNet;
using SuperSoccerShowdown.PlayerGenerator.Config;
using SuperSoccerShowdown.PlayerGenerator.Universes;
using SuperSoccerShowdown.PlayerGenerator.Universes.Interfaces;
using SuperSoccerShowdown.PlayerGenerator.Universes.Poke;
using SuperSoccerShowdown.PlayerGenerator.Universes.StarWars;
using SuperSoccerShowdown.Common;
using SuperSoccerShowdown.PlayerGenerator.Models;


// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace PlayerGenerator;

public partial class PlayerGeneratorFunction
{
    private ServiceCollection _serviceCollection;
    private readonly ILogger _logger;

    public PlayerGeneratorFunction()
    {
        using var loggerFactory = LoggerFactory.Create(builder =>
        {
            builder
                .SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Information)
                .AddConsole()
                .AddDebug();
        });

        _logger = loggerFactory.CreateLogger(nameof(PlayerGeneratorFunction));
        ConfigureServices();
    }


    public async Task<PlayerGeneratorResponse> FunctionHandler(PlayerGeneratorRequest request, ILambdaContext context)
    {
        using ServiceProvider serviceProvider = _serviceCollection.BuildServiceProvider();
        var factory = serviceProvider.GetRequiredService<IUniverseFactory>();

        var universe = factory.Create(request.UniverseType);

        _logger.LogInformation($"Generating players for universe: {universe.Type}");
        var result = await universe.GetPlayersAsync(5);

        return new PlayerGeneratorResponse
        {
            Players = result
        };
    }

    private void ConfigureServices()
    {
        _serviceCollection = new ServiceCollection();

        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
        _serviceCollection.Configure<SwapiClientSettings>(config.GetSection("SwapiClientSettings"));
        var swapiClientSettings = config.GetSection("SwapiClientSettings").Get<SwapiClientSettings>();

        _serviceCollection.AddSingleton<PokeApiClient>();
        _serviceCollection.AddSingleton<PokemonClient>();
        _serviceCollection.AddTransient<IUniverse, PokemonUniverse>();


        _serviceCollection.AddHttpClient<StarWarsClient>()
            .ConfigureHttpClient((provider, client) =>
            {
                client.BaseAddress = new Uri(swapiClientSettings.BaseAddress);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            });
        _serviceCollection.AddTransient<IUniverse, StarWarsUniverse>();
        
        _serviceCollection.AddTransient<IUniverseFactory, UniverseFactory>();

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
