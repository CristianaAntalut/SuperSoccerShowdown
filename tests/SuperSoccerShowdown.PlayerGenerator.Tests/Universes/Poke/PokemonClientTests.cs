using Microsoft.Extensions.Logging.Abstractions;
using PokeApiNet;
using RichardSzalay.MockHttp;
using SuperSoccerShowdown.PlayerGenerator.Universes.Poke;
using System.Net;
using System.Text.Json;
using Xunit;

namespace SuperSoccerShowdown.PlayerGenerator.Tests.Universes.Poke;

/// <summary>
/// used samples from:
/// https://github.com/jtwotimes/PokeApiNet/blob/master/PokeApiNet.Tests/PokeApiClientTests.cs
/// </summary>

[Trait("Category", "UnitTests")]
public class PokemonClientTests
{
    private NullLogger<PokemonClient> _logger;
    private readonly MockHttpMessageHandler _mockHttp;
    private PokeApiClient externalApi;
    private PokemonClient _sut;

    public PokemonClientTests()
    {
        _mockHttp = new MockHttpMessageHandler();
        externalApi = new(_mockHttp);
        _logger = NullLogger<PokemonClient>.Instance;
        _sut = new PokemonClient(_logger, externalApi);
    }

    [Fact]
    public async Task GetNumberOfAvailablePlayersAsync_ReturnsValue()
    {
        var pokemonPage = new NamedApiResourceList<Pokemon>()
        {
            Results = new()
            {
                new NamedApiResource<Pokemon> { Name = "bulbasaur", Url = "https://pokeapi.co/api/v2/pokemon/1/" },
                new NamedApiResource<Pokemon> { Name = "ivysaur", Url = "https://pokeapi.co/api/v2/pokemon/2/" }
            }
        };

        _mockHttp.Expect("*pokemon")
            .Respond("application/json", JsonSerializer.Serialize(pokemonPage));

        var result = await _sut.GetNumberOfAvailablePlayersAsync();

        Assert.Equal(pokemonPage.Count, result);
    }

    [Fact]
    public async Task GetNumberOfAvailablePlayersAsync_GivenException_CatchesException()
    {
        // assemble
        _mockHttp.When("*pokemon")
            .Respond(HttpStatusCode.NotFound);

        // act / assert
        await Assert.ThrowsAsync<Exception>(_sut.GetNumberOfAvailablePlayersAsync);
    }

    [Fact]
    public async Task GetPlayerAsync_ReturnsMappedPlayerDto_WhenApiSucceeds()
    {
        var expectedJson = @"{
            ""id"": 1,
            ""name"": ""bulbasaur"",
            ""base_experience"": 64
        }";

        _mockHttp.Expect("https://pokeapi.co/api/v2/pokemon/1*")
                .Respond("application/json", expectedJson);

        var httpClient  = new HttpClient(_mockHttp);
        var pokeApi = new PokeApiClient(httpClient);
        var sut = new PokemonClient(_logger, pokeApi);

        var result = await sut.GetPlayerAsync(1);

        Assert.Equal(1, result?.Id);
        Assert.Equal("bulbasaur", result?.Name);
    }

    [Fact]
    public async Task GetPlayerAsync_ReturnsNull_WhenApiThrows()
    {
        _mockHttp.When("*pokemon")
            .Throw(new Exception());

        var result = await _sut.GetPlayerAsync(1);
        
        Assert.Null(result);
    }
}
