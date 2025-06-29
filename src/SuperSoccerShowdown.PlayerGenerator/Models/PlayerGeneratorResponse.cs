using SuperSoccerShowdown.Common.Dtos;

namespace SuperSoccerShowdown.PlayerGenerator.Models;
public class PlayerGeneratorResponse
{
    public IList<PlayerDto> Players { get; set; }
}

