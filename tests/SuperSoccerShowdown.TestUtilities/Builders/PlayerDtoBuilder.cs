using SuperSoccerShowdown.Common.Dtos;
using SuperSoccerShowdown.Common.Utilities;

namespace SuperSoccerShowdown.TestUtilities.Builders;

public class PlayerDtoBuilder
{
    private string _name = RandomGenerator.GenerateUniqueRandomString(10);
    private int _height = RandomGenerator.GenerateUniqueRandomNumber(10, 200);
    private int _weight = RandomGenerator.GenerateUniqueRandomNumber(1, 200);

    public PlayerDto Build()
    {
        return new PlayerDto
        {
            Name = _name,
            Height = _height,
            Weight = _weight
        };
    }
    public PlayerDtoBuilder WithName(string name)
    {
        _name = name;
        return this;
    }

    public PlayerDtoBuilder WithHeight(int height)
    {
        _height = height;
        return this;
    }

    public PlayerDtoBuilder WithWeight(int weight)
    {
        _weight = weight;
        return this;
    }
}
