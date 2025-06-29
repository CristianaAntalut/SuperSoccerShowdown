using Moq;
using SuperSoccerShowdown.Common.Dtos;
using SuperSoccerShowdown.PlayerGenerator.Universes;
using SuperSoccerShowdown.PlayerGenerator.Universes.Interfaces;
using Xunit;

namespace SuperSoccerShowdown.PlayerGenerator.Tests.Universes;

public class UniverseFactoryTests
{
    [Fact]
    public void Create_GivenValidUniverseType_ReturnsRegisteredUniverse()
    {
        // Arrange
        var mockUniverse = new Mock<IUniverse>();
        mockUniverse.Setup(u => u.Type).Returns(UniverseType.Pokemon);
        var factory = new UniverseFactory(new List<IUniverse> { mockUniverse.Object });

        // Act
        var result = factory.Create(UniverseType.Pokemon);

        // Assert
        Assert.Same(mockUniverse.Object, result);
    }

    [Fact]
    public void Create_GivenUnregisteredType_ThrowsArgumentException()
    {
        // Arrange
        var mockUniverse = new Mock<IUniverse>();
        mockUniverse.Setup(u => u.Type).Returns(UniverseType.Pokemon);
        var factory = new UniverseFactory(new List<IUniverse> { mockUniverse.Object });

        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => factory.Create(UniverseType.StarWars));
        Assert.Contains("not registered", ex.Message, StringComparison.OrdinalIgnoreCase);
    }
}


