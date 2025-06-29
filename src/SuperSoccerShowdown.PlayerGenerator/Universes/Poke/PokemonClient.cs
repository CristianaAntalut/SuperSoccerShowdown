using Microsoft.Extensions.Logging;
using PokeApiNet;
using SuperSoccerShowdown.Common.Dtos;
using SuperSoccerShowdown.PlayerGenerator.Universes.Interfaces;

namespace SuperSoccerShowdown.PlayerGenerator.Universes.Poke;

public class PokemonClient(ILogger<PokemonClient> logger, PokeApiClient pokeClient) : IUniverseClient
{
    private readonly ILogger<PokemonClient> _logger = logger;
    private readonly PokeApiClient _pokeClient = pokeClient;
    
    public async Task<int> GetNumberOfAvailablePlayersAsync()
    {
        try
        {
            var page = await _pokeClient.GetNamedResourcePageAsync<Pokemon>();

            return page.Count;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while fetching number of available players from Pokemon API");
            throw new Exception($"{nameof(PokemonClient)} thowed an exception: {ex.Message}");
        }
    }

    public async Task<PlayerDto?> GetPlayerAsync(int id)
    {
        try
        {
            var pokemon = await _pokeClient.GetResourceAsync<Pokemon>(id);
           return PokemonMapper.MapToPlayer(pokemon);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while fetching player with ID {Id} from Pokemon API", id);
            return null;
        }
    }
}
