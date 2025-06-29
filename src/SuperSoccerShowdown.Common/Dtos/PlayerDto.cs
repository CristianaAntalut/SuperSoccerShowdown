namespace SuperSoccerShowdown.Common.Dtos;
public class PlayerDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Weight { get; set; }
    public int Height { get; set; }

    public PlayerType PlayerType { get; set; }
}
