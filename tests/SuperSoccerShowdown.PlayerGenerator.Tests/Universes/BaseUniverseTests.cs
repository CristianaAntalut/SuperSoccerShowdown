using Moq;
using SuperSoccerShowdown.Common.Dtos;
using SuperSoccerShowdown.PlayerGenerator.Universes;
using SuperSoccerShowdown.PlayerGenerator.Universes.Interfaces;
using SuperSoccerShowdown.TestUtilities.Builders;
using Xunit;

namespace SuperSoccerShowdown.PlayerGenerator.Tests.Universes
{
    public class BaseUniverseTests
    {
        private Mock<IUniverseClient> _mockClient;
        private TestUniverse _sut;

        public BaseUniverseTests()
        {
            _mockClient = new Mock<IUniverseClient>();
            _sut = new TestUniverse(_mockClient.Object);
        }


        [Fact]
        public void IsPlayerValid_NullDto_ReturnsFalse()
        {
            var result = _sut.IsPlayerValid(null);
            Assert.False(result);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void IsPlayerValid_InvalidName_ReturnsFalse(string name)
        {
            var dto = new PlayerDtoBuilder()
                .WithName(name)
                .Build();

            var result = _sut.IsPlayerValid(dto);

            Assert.False(result);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-10)]
        public void IsPlayerValid_InvalidHeight_ReturnsFalse(int height)
        {
            var dto = new PlayerDtoBuilder()
                .WithHeight(height)
                .Build();

            var result = _sut.IsPlayerValid(dto);

            Assert.False(result);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-5)]
        public void IsPlayerValid_InvalidWeight_ReturnsFalse(int weight)
        {
            var dto = new PlayerDtoBuilder()
                .WithWeight(weight)
                .Build();

            var result = _sut.IsPlayerValid(dto);

            Assert.False(result);
        }

        [Fact]
        public void IsPlayerValid_ValidPlayer_ReturnsTrue()
        {
            var dto = new PlayerDtoBuilder().Build();

            var result = _sut.IsPlayerValid(dto);

            Assert.True(result);
        }

        [Fact]
        public async Task GetPlayersAsync_ReturnsExpectedNumberOfUniqueValidPlayers()
        {
            // Arrange
            int availablePlayers = 10;
            int teamMembers = 5;
            var ids = new Queue<int>(Enumerable.Range(1, availablePlayers));

            _mockClient.Setup(x => x.GetNumberOfAvailablePlayersAsync()).ReturnsAsync(availablePlayers);

            _mockClient
                .Setup(x => x.GetPlayerAsync(It.Is<int>(x => x % 2 == 0)))
                .ReturnsAsync(() => null);

            _mockClient.Setup(x => x.GetPlayerAsync(It.Is<int>(x => x % 2 != 0)))
                .ReturnsAsync((int id) => new PlayerDto { Id = id, Name = $"Player{id}", Height = 10, Weight = 10 });

            // Act
            var result = await _sut.GetPlayersAsync(teamMembers);

            // Assert
            Assert.Equal(teamMembers, result.Count);
            Assert.Equal(result.Count, result.Select(p => p.Id).Distinct().Count());
            Assert.True(result.All(p => p.Id % 2 != 0), "All players should have odd IDs");
            Assert.All(result, p => Assert.False(string.IsNullOrEmpty(p.Name)));
            Assert.All(result, p => Assert.True(p.Height > 0));
            Assert.All(result, p => Assert.True(p.Weight > 0));
        }

        class TestUniverse : BaseUniverse
        {
            public TestUniverse(IUniverseClient client) : base(client)
            {
            }
            public override UniverseType Type => UniverseType.Pokemon;
           
        }
    }
}
