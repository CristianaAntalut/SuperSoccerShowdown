using SuperSoccerShowdown.Common.Dtos;

namespace SuperSoccerShowdown.Common.Models.GameSimulator;

public class GameSimulatorRequest
{
    public List<PlayerDto> FirstTeam { get; set; }
    public List<PlayerDto> SecondTeam { get; set; } 
    
}
