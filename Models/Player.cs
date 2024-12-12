namespace TennisPlayersStats.Models;

public class Player
{
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string ShortName { get; set; } = null!;
    public string Sex { get; set; } = null!;
    public Country Country { get; set; } = null!;
    public string Picture { get; set; } = null!;
    public PlayerData Data { get; set; } = null!;
}