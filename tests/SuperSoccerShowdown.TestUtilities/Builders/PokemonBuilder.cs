using PokeApiNet;
using SuperSoccerShowdown.Common.Utilities;

namespace SuperSoccerShowdown.TestUtilities.Builders;

public class PokemonBuilder
{
    private int _id = RandomGenerator.GenerateUniqueRandomNumber(1, 10);
    private string _name = RandomGenerator.GenerateUniqueRandomString(10);
    private int _height = RandomGenerator.GenerateUniqueRandomNumber(10, 200);
    private int _weight = RandomGenerator.GenerateUniqueRandomNumber(1, 200);
    public Pokemon Build()
    {
        return new Pokemon
        {
            Id = _id,
            Name = _name,
            Height = _height,
            Weight = _weight
        };
    }
    public PokemonBuilder WithId(int id)
    {
        _id = id;
        return this;
    }
    public PokemonBuilder WithName(string name)
    {
        _name = name;
        return this;
    }

    public PokemonBuilder WithHeight(int height)
    {
        _height = height;
        return this;
    }

    public PokemonBuilder WithWeight(int weight)
    {
        _weight = weight;
        return this;
    }
}