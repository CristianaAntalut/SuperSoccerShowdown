using SuperSoccerShowdown.Common.Dtos;

namespace SuperSoccerShowdown.GameSimulator;

public interface IGameService
{
    List<string> Play(List<PlayerDto> firstTeam, List<PlayerDto> secondTeam);
}
