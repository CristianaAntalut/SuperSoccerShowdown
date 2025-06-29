using SuperSoccerShowdown.Common.Dtos;

namespace SuperSoccerShowdown.Common.Models.TeamGenerator;

public class TeamGeneratorRequest
{
    public List<PlayerDto> Players { get; set; }
    public StrategyType StrategyType { get; set; }
    public int numberOfOffencePlayers { get; set; }
}

