using SuperSoccerShowdown.Common.Dtos;

namespace SuperSoccerShowdown.PlayerGenerator.Universes.Poke;

public class PokemonUniverse : BaseUniverse
{
    public PokemonUniverse(PokemonClient client) : base(client)
    {
    }
            
    public override UniverseType Type => UniverseType.Pokemon;
}


