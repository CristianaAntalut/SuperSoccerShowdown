using SuperSoccerShowdown.Common.Dtos;

namespace SuperSoccerShowdown.PlayerGenerator.Universes.Interfaces
{
    public interface IUniverseFactory
    {
        IUniverse Create(UniverseType universeType);
    }
}
