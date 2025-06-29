using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using PokeApiNet;
using SuperSoccerShowdown.Common.Dtos;
using SuperSoccerShowdown.PlayerGenerator.Universes;
using SuperSoccerShowdown.PlayerGenerator.Universes.Poke;
using Xunit;

namespace SuperSoccerShowdown.PlayerGenerator.Tests.Universes.Poke;

[Trait("Category", "UnitTests")]
public class PokemonUniverseTests
{
    private readonly PokemonUniverse _sut;
    private NullLogger<PokemonClient> _logger;
    private Mock<PokeApiClient> _mockPokeApiClient;
    private readonly Mock<PokemonClient> _mockClient;

    public PokemonUniverseTests()
    {
        _logger = NullLogger<PokemonClient>.Instance;
        _mockPokeApiClient = new Mock<PokeApiClient>();
        _mockClient = new Mock<PokemonClient>(_logger, _mockPokeApiClient.Object);
        _sut = new PokemonUniverse(_mockClient.Object);
    }

    [Fact]
    public void PokemonUniverse_Type_ReturnsPokemon()
    {
        Assert.Equal(UniverseType.Pokemon, _sut.Type);
    }

    [Fact]
    public void PokemonUniverse_Inherits_BaseUniverse()
    {
        Assert.True(typeof(BaseUniverse).IsAssignableFrom(typeof(PokemonUniverse)));
    }

    [Fact]
    public void PokemonUniverse_DoesNotOverride_GetPlayersAsync()
    {
        var method = typeof(PokemonUniverse).GetMethod("GetPlayersAsync");
        Assert.True(method?.DeclaringType == typeof(BaseUniverse));
    }

    [Fact]
    public void PokemonUniverse_DoesNotOverride_IsPlayerValid()
    {
        var method = typeof(PokemonUniverse).GetMethod("IsPlayerValid");
        Assert.True(method?.DeclaringType == typeof(BaseUniverse));
    }
}