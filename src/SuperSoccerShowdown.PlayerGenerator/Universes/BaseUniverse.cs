using Microsoft.Extensions.Logging;
using SuperSoccerShowdown.Common.Dtos;
using SuperSoccerShowdown.Common.Utilities;
using SuperSoccerShowdown.PlayerGenerator.Universes.Interfaces;

namespace SuperSoccerShowdown.PlayerGenerator.Universes;

public abstract class BaseUniverse(IUniverseClient client) : IUniverse
{
    private readonly IUniverseClient _client = client;
    public abstract UniverseType Type { get; }

    public virtual async Task<List<PlayerDto>> GetPlayersAsync(int numberOfTeamPlayers)
    {
        var availablePlayers = await _client.GetNumberOfAvailablePlayersAsync();

        var listOfPlayers = new List<PlayerDto>();
        var counter = 0;
        while (counter < numberOfTeamPlayers)
        {
            var id = RandomGenerator.GenerateUniqueRandomNumber(1, availablePlayers);
            bool hasDuplicateId = listOfPlayers.Any(p => p.Id == id);

            if (!hasDuplicateId)
            {
                var player = await _client.GetPlayerAsync(id);
                if (player != null && IsPlayerValid(player))
                {
                    listOfPlayers.Add(player);
                    counter++;
                }
            }
        }

        return listOfPlayers;
    }


    public virtual bool IsPlayerValid(PlayerDto player)
    {
        if (player is null) return false;
        if (string.IsNullOrEmpty(player.Name)) return false;
        if (player.Height < 1) return false;
        if (player.Weight < 1) return false;

        return true;
    }
}
