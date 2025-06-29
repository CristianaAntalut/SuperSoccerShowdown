using SuperSoccerShowdown.Common.Dtos;

namespace SuperSoccerShowdown.TeamGenerator.Interfaces;

public interface ITeamContext
{
    List<PlayerDto> GenerateTeam(StrategyType strategyType, List<PlayerDto> players, int numberOfOffencePlayers);
}
