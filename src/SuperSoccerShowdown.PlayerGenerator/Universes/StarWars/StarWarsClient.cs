using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SuperSoccerShowdown.PlayerGenerator.Config;
using SuperSoccerShowdown.Common.Dtos;
using SuperSoccerShowdown.PlayerGenerator.Universes.Interfaces;

namespace SuperSoccerShowdown.PlayerGenerator.Universes.StarWars;

public class StarWarsClient
    (ILogger<StarWarsClient> logger, HttpClient client, IOptionsMonitor<SwapiClientSettings> optionsMonitor) 
    : IUniverseClient
{
    private readonly ILogger<StarWarsClient> _logger = logger;
    private readonly HttpClient _client = client;
    private readonly SwapiClientSettings _settings = optionsMonitor.CurrentValue;

    public async Task<int> GetNumberOfAvailablePlayersAsync()
    {
        try
        {
            string json = await _client.GetStringAsync(_settings.BaseAddress);
            var total = StarWarsMapper.MapToTotalRecords(json);

            return total;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while fetching number of available players from Star Wars API");
            throw new Exception($"{nameof(StarWarsClient)} thowed an exception: {ex.Message}");
        }
    }

    public async Task<PlayerDto?> GetPlayerAsync(int Id)
    {
        try
        {
            var url = $"people/{Id}";
            var response = await _client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
               return StarWarsMapper.MapToPlayerDto(json);
            }

            return null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while fetching player with ID {Id} from Star Wars API", Id);
            return null;
        }
    }
}
