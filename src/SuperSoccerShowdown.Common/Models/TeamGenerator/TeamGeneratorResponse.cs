using SuperSoccerShowdown.Common.Dtos;

namespace SuperSoccerShowdown.Common.Models.TeamGenerator;

public class TeamGeneratorResponse
{
    public IList<PlayerDto> Players { get; set; }
}

