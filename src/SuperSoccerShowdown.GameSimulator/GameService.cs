using Microsoft.Extensions.Options;
using SuperSoccerShowdown.Common.Dtos;

namespace SuperSoccerShowdown.GameSimulator;

public class GameService : IGameService
{
    private readonly GameConfig _gameConfig;
    public GameService()
    {
        _gameConfig = new GameConfig();
    }
    public GameService( IOptionsMonitor<GameConfig> optionsMonitor)
    {
        _gameConfig = optionsMonitor.CurrentValue ?? new GameConfig();
    }

    public List<string> Play(List<PlayerDto> firstTeam, List<PlayerDto> secondTeam)
    {
        var gameHighlights = new List<string>();

        var rnd = new Random();
        var allCombinations =
            (from playerFirstTeam in firstTeam
             from playerSecondTeam in secondTeam
             select (playerFirstTeam, playerSecondTeam))
            .ToList();

        int take = Math.Min(_gameConfig.NumberOfTurns, allCombinations.Count);

        var uniqueRandomPairs = allCombinations
            .OrderBy(_ => rnd.Next())
            .Take(take)
            .ToList();


        foreach (var combo in uniqueRandomPairs)
        {
            var hitType = (rnd.Next(2) == 0) ? "scores" : "misses";
            var (player1, player2) = (rnd.Next(2) == 0)
                ? (combo.playerFirstTeam.Name, combo.playerSecondTeam.Name)
                : (combo.playerSecondTeam.Name, combo.playerFirstTeam.Name);
            gameHighlights.Add($"{player1} {hitType} against {player2}");
        }

        return gameHighlights;
    }

}