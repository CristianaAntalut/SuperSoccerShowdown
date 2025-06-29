using SuperSoccerShowdown.Common.Dtos;
using SuperSoccerShowdown.PlayerGenerator.Universes.Interfaces;

namespace SuperSoccerShowdown.PlayerGenerator.Universes;

public class UniverseFactory : IUniverseFactory
{
    private readonly IDictionary<UniverseType, IUniverse> _universes;

    public UniverseFactory(IEnumerable<IUniverse> universes)
    {
        _universes = universes.ToDictionary(
            u => u.Type,
            u => u
        );
    }

    public IUniverse Create(UniverseType universeType)
    {
        if (_universes.TryGetValue(universeType, out var universe))
            return universe;

        throw new ArgumentException($"BaseUniverse type '{universeType}' not registered.");
    }
}

