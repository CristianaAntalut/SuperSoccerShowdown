using SuperSoccerShowdown.Common.Dtos;

namespace SuperSoccerShowdown.PlayerGenerator.Universes.StarWars;
public class StarWarsUniverse(StarWarsClient client) : BaseUniverse(client)
{
    public override UniverseType Type => UniverseType.StarWars;

}

