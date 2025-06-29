using SuperSoccerShowdown.Common.Utilities;
using SuperSoccerShowdown.PlayerGenerator.Universes.Poke;
using SuperSoccerShowdown.TestUtilities.Builders;
using Xunit;

namespace SuperSoccerShowdown.PlayerGenerator.Tests.Universes.Poke;

[Trait("Category", "UnitTests")]
public class PokemonMapperTests
{
    [Fact]
    public void MapToPlayer_MapsAllFieldsCorrectly()
    {
        // Arrange
        var pokemon = new PokemonBuilder().Build();

        // Act
        var player = pokemon.MapToPlayer();

        // Assert
        Assert.Equal(pokemon.Id, player.Id);
        Assert.Equal(pokemon.Name, player.Name);
        Assert.Equal(pokemon.Height, player.Height);
        Assert.Equal(pokemon.Weight, player.Weight);
    }

    [Fact]
    public void MapToPlayer_Id_MapsCorrectly()
    {
        var pokemon = new PokemonBuilder()
            .WithId(5)
            .Build();

        var player = pokemon.MapToPlayer();

        Assert.Equal(pokemon.Id, player.Id);
    }

    [Fact]
    public void MapToPlayer_Name_MapsCorrectly()
    {
        var pokemon = new PokemonBuilder()
            .WithName(RandomGenerator.GenerateUniqueRandomString(10))
            .Build();

        var player = pokemon.MapToPlayer();

        Assert.Equal(pokemon.Name, player.Name);
    }

    [Fact]
    public void MapToPlayer_Height_MapsCorrectly()
    {
        var pokemon = new PokemonBuilder()
            .WithHeight(RandomGenerator.GenerateUniqueRandomNumber(1, 100))
            .Build();

        var player = pokemon.MapToPlayer();

        Assert.Equal(pokemon.Height, player.Height);
    }

    [Fact]
    public void MapToPlayer_Weight_MapsCorrectly()
    {
        var pokemon = new PokemonBuilder()
            .WithWeight(RandomGenerator.GenerateUniqueRandomNumber(1, 100))
            .Build();

        var player = pokemon.MapToPlayer();

        Assert.Equal(pokemon.Weight, player.Weight);
    }
}
