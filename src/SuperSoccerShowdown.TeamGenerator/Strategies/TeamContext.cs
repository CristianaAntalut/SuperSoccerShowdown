using Microsoft.Extensions.Logging;
using SuperSoccerShowdown.Common.Dtos;
using SuperSoccerShowdown.TeamGenerator.Interfaces;

namespace SuperSoccerShowdown.TeamGenerator.Strategies;

public class TeamContext : ITeamContext
{
    private readonly IStrategyFactory _strategyFactory;
    private readonly ILogger<TeamContext> _logger;

    public TeamContext(ILogger<TeamContext> logger, IStrategyFactory strategyFactory)
    {
        _logger = logger;
        _strategyFactory = strategyFactory;
    }

    public List<PlayerDto> GenerateTeam(StrategyType strategyType, List<PlayerDto> players, int numberOfOffencePlayers)
    {
        var strategy = _strategyFactory.Create(strategyType);
        if (!IsInputValid(players, numberOfOffencePlayers))
        {
            _logger.LogError("Invalid input for team generation.");
            throw new ArgumentException("Invalid input for team generation.");
        }
        return strategy.GenerateTeam(players,numberOfOffencePlayers);
    }

    public bool IsInputValid(List<PlayerDto> players, int numberOfOffencePlayers)
    {
        if (players == null) return false;
        if (players.Count <= numberOfOffencePlayers) return false;

        return true;
    }
}
