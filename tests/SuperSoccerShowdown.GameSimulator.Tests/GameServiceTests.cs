using Microsoft.Extensions.Options;
using Moq;
using SuperSoccerShowdown.Common.Dtos;
using SuperSoccerShowdown.TestUtilities.Builders;
using Xunit;

namespace SuperSoccerShowdown.GameSimulator.Tests;

public class GameServiceTests
{

    [Fact]
    public void Play_GivenDefaultConfig_ReturnsExpectedNumberOfHighlights()
    {
        // Arrange
        var sut = new GameService();
        var firstTeam = new List<PlayerDto>
        {
            new PlayerDtoBuilder().WithName("Alice").Build(),
            new PlayerDtoBuilder().WithName("Bob").Build(),
            new PlayerDtoBuilder().WithName("Charlie").Build(),
            new PlayerDtoBuilder().WithName("Dana").Build()
        };
        var secondTeam = new List<PlayerDto>
        {
            new PlayerDtoBuilder().WithName("Frank").Build(),
            new PlayerDtoBuilder().WithName("George").Build(),
            new PlayerDtoBuilder().WithName("Elise").Build(),
            new PlayerDtoBuilder().WithName("Jack").Build()
        };


        // Act
        var result = sut.Play(firstTeam, secondTeam);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(10, result.Count);
    }

    [Fact]
    public void Play_GivenConfig_ReturnsExpectedNumberOfHighlights()
    {
        // Arrange
        var config = new GameConfig { NumberOfTurns = 3 };
        var mockOptionsMonitor = new Mock<IOptionsMonitor<GameConfig>>();
        mockOptionsMonitor.Setup(x => x.CurrentValue).Returns(new GameConfig
        {
            NumberOfTurns = 3
        });
        var sut = new GameService(mockOptionsMonitor.Object);

        var firstTeam = new List<PlayerDto>
        {
            new PlayerDtoBuilder().WithName("Alice").Build(),
            new PlayerDtoBuilder().WithName("Bob").Build()
        };
        var secondTeam = new List<PlayerDto>
        {
            new PlayerDtoBuilder().WithName("Charlie").Build(),
            new PlayerDtoBuilder().WithName("Dana").Build()
        };


        // Act
        var result = sut.Play(firstTeam, secondTeam);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(3, result.Count);
    }

    [Fact]
    public void Play_HandlesEmptyTeams()
    {
        var sut = new GameService();

        var result = sut.Play(new List<PlayerDto>(), new List<PlayerDto>());

        Assert.Empty(result);
    }

    [Fact]
    public void Play_HandlesEmptyTeam()
    {
        var sut = new GameService();

        var result = sut.Play(new List<PlayerDto>(), new List<PlayerDto> { new PlayerDto { Name = "A" } });

        Assert.Empty(result);
    }

    [Fact]
    public void Play_NumberOfTurnsGreaterThanCombinations_TakesAllCombinations()
    { 
        var sut = new GameService();

        var firstTeam = new List<PlayerDto> { new PlayerDto { Name = "A" } };
        var secondTeam = new List<PlayerDto> { new PlayerDto { Name = "B" }, new PlayerDto { Name = "C" } };

        var result = sut.Play(firstTeam, secondTeam);

        Assert.Equal(2, result.Count);
    }

    [Fact]
    public void Play_GameHighlightsContainKeywords()
    {
        var sut = new GameService();

        var firstTeam = new List<PlayerDto> { new PlayerDto { Name = "A" } };
        var secondTeam = new List<PlayerDto> { new PlayerDto { Name = "B" }, new PlayerDto { Name = "C" } };

        var result = sut.Play(firstTeam, secondTeam);

        Assert.All(result, s => Assert.True(s.Contains("scores") || s.Contains("misses")));
    }
}
