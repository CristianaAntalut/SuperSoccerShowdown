using SuperSoccerShowdown.Common.Utilities;
using Xunit;

namespace SuperSoccerShowdown.PlayerGenerator.Tests.Utilities;

public class RandomNumberGeneratorTests
{
    [Fact]
    public void GenerateUniqueRandomNumber_ValidRange_ReturnsNumberWithinRange()
    {
        int min = 10;
        int max = 20;

        int result = RandomGenerator.GenerateUniqueRandomNumber(min, max);

        Assert.InRange(result, min, max - 1);
    }

    [Fact]
    public void GenerateUniqueRandomNumber_MaxLessThanMin_ThrowsArgumentException()
    {
        int min = 10;
        int max = 5;

        Assert.Throws<ArgumentException>(() =>
            RandomGenerator.GenerateUniqueRandomNumber(min, max));
    }

}
