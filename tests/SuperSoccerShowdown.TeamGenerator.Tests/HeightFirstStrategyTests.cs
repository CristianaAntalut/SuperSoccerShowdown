using SuperSoccerShowdown.Common.Dtos;
using SuperSoccerShowdown.TeamGenerator.Strategies;
using SuperSoccerShowdown.TestUtilities.Builders;
using Xunit;

namespace SuperSoccerShowdown.TeamGenerator.Tests;

public class HeightFirstStrategyTests
{
    [Fact]
    public void GenerateTeam_AssignsGoalieToTallestPlayer()
    {
        var players = new List<PlayerDto>
        {
            new PlayerDtoBuilder().WithName("A").WithHeight(180).WithWeight(70).Build(),
            new PlayerDtoBuilder().WithName("B").WithHeight(190).WithWeight(80).Build(), 
            new PlayerDtoBuilder().WithName("C").WithHeight(175).WithWeight(75).Build(),
        };
        var strategy = new HeightFirstStrategy();
        var result = strategy.GenerateTeam(players, 1);
        var goalie = result.Single(p => p.PlayerType == PlayerType.Goalie);
        Assert.Equal("B", goalie.Name);
    }

    [Fact]
    public void GenerateTeam_AssignsGoalieToTallestAndLightestPlayer()
    {
        var players = new List<PlayerDto>
        {
            new PlayerDtoBuilder().WithName("A").WithHeight(180).WithWeight(70).Build(),
            new PlayerDtoBuilder().WithName("B").WithHeight(180).WithWeight(80).Build(),
            new PlayerDtoBuilder().WithName("C").WithHeight(175).WithWeight(75).Build(),
        };
        var strategy = new HeightFirstStrategy();
        var result = strategy.GenerateTeam(players, 1);
        var goalie = result.Single(p => p.PlayerType == PlayerType.Goalie);
        Assert.Equal("A", goalie.Name);
    }


    [Theory]
    [InlineData(2, 2, 1)]
    [InlineData(1, 1, 2)]
    [InlineData(3, 3, 0)]
    [InlineData(0, 0, 3)]
    public void GenerateTeam_AssignsCorrectNumberOfOffenceAndDefence(int numberOfOffence, int expectedOffence, int expectedDefence)
    {
        var players = new List<PlayerDto>
        {
            new PlayerDtoBuilder().WithName("A").WithHeight(180).WithWeight(70).Build(),
            new PlayerDtoBuilder().WithName("B").WithHeight(190).WithWeight(80).Build(),
            new PlayerDtoBuilder().WithName("C").WithHeight(175).WithWeight(75).Build(),
            new PlayerDtoBuilder().WithName("D").WithHeight(170).WithWeight(65).Build(),
        };
        var strategy = new HeightFirstStrategy();

        var result = strategy.GenerateTeam(players, numberOfOffence);

        Assert.Equal(expectedOffence, result.Count(p => p.PlayerType == PlayerType.Offence));
        Assert.Equal(expectedDefence, result.Count(p => p.PlayerType == PlayerType.Defence));
        Assert.Equal(1, result.Count(p => p.PlayerType == PlayerType.Goalie));
    }

    [Fact]
    public void GenerateTeam_OrdersByHeightThenWeight()
    {
        var players = new List<PlayerDto>
        {
            new PlayerDtoBuilder().WithName("A").WithHeight(180).WithWeight(70).Build(),
            new PlayerDtoBuilder().WithName("B").WithHeight(180).WithWeight(80).Build(), 
            new PlayerDtoBuilder().WithName("C").WithHeight(175).WithWeight(75).Build(),
        };
        var strategy = new HeightFirstStrategy();
        var result = strategy.GenerateTeam(players, 1);

        //code order is defence, offence, goalie in returned list is
        Assert.Equal("B", result[0].Name);
        Assert.Equal(PlayerType.Defence, result[0].PlayerType);
        Assert.Equal("C", result[1].Name);
        Assert.Equal(PlayerType.Offence, result[1].PlayerType);
        Assert.Equal("A", result[2].Name);
        Assert.Equal(PlayerType.Goalie, result[2].PlayerType);
    }

    [Fact]
    public void GenerateTeam_ReturnsAllPlayersWithAssignedTypes()
    {
        var players = new List<PlayerDto>
        {
            new PlayerDtoBuilder().WithName("A").WithHeight(180).WithWeight(70).Build(),
            new PlayerDtoBuilder().WithName("B").WithHeight(190).WithWeight(80).Build(),
            new PlayerDtoBuilder().WithName("C").WithHeight(175).WithWeight(75).Build(),
        };
        var strategy = new HeightFirstStrategy();
        var result = strategy.GenerateTeam(players, 1);
        Assert.All(result, p => Assert.NotEqual(PlayerType.Unknown, p.PlayerType));
        Assert.Equal(players.Count, result.Count);
    }
}
