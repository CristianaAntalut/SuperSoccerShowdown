using SuperSoccerShowdown.Common.Dtos;

namespace SuperSoccerShowdown.TeamGenerator.Interfaces;

public interface ITeamStrategy
{
    StrategyType Type { get; }
    List<PlayerDto> GenerateTeam(List<PlayerDto> players, int numberOfOffencePlayers);
}
