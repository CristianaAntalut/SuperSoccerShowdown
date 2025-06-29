using SuperSoccerShowdown.Common.Dtos;

namespace SuperSoccerShowdown.TeamGenerator.Models;

public class TeamGeneratorResponse
{
    public IList<PlayerDto> Players { get; set; }
}

