using SuperSoccerShowdown.Common.Dtos;
using SuperSoccerShowdown.TeamGenerator.Interfaces;

namespace SuperSoccerShowdown.TeamGenerator.Strategies;

public class WeightFirstStrategy : ITeamStrategy
{
    public StrategyType Type => StrategyType.WeightFirst;

    public List<PlayerDto> GenerateTeam(List<PlayerDto> players, int numberOfOffencePlayers)
    {
        var orderedPlayers = players.OrderByDescending(x => x.Weight)
              .ThenByDescending(x => x.Height)
              .ToList();

        var goalie = orderedPlayers.MaxBy(x => x.Height);
        goalie.PlayerType = PlayerType.Goalie;


        var restOfPlayers = orderedPlayers.Where(x => x != goalie).ToList();
        var numberOfDefencePlayers = restOfPlayers.Count - numberOfOffencePlayers;

        foreach (var player in restOfPlayers.Take(numberOfDefencePlayers))
        {
            player.PlayerType = PlayerType.Defence;
        }

        foreach (var player in restOfPlayers.Skip(numberOfDefencePlayers))
        {
            player.PlayerType = PlayerType.Offence;
        }
        restOfPlayers.Add(goalie);
        return restOfPlayers;
    }
}
