using SuperSoccerShowdown.Common.Dtos;

namespace SuperSoccerShowdown.GameSimulator.Models;

public class GameSimulatorRequest
{
    public List<PlayerDto> FirstTeam { get; set; }
    public List<PlayerDto> SecondTeam { get; set; } 
}
