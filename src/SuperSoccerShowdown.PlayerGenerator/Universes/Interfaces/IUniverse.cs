using SuperSoccerShowdown.Common.Dtos;

namespace SuperSoccerShowdown.PlayerGenerator.Universes.Interfaces;

public interface IUniverse
{
    UniverseType Type { get; }
    Task<List<PlayerDto>> GetPlayersAsync(int numberOfTeamMembers);

}
