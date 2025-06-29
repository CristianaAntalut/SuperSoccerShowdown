using SuperSoccerShowdown.Common.Dtos;

namespace SuperSoccerShowdown.PlayerGenerator.Universes.Interfaces;

public interface IUniverseClient
{
    public Task<PlayerDto?> GetPlayerAsync(int Id);
    public Task<int> GetNumberOfAvailablePlayersAsync();
}
