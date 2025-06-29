using SuperSoccerShowdown.Common.Dtos;

namespace SuperSoccerShowdown.TeamGenerator.Interfaces;

public interface IStrategyFactory
{
    ITeamStrategy Create(StrategyType type);
}
