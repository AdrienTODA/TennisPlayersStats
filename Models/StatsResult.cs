namespace TennisPlayersStats.Models;

public class StatsResult
{
	public Country? CountryWithMostWins { get; set; }
	public float PlayersAverageBMI { get; set; }
	public int PlayersMedianHeight { get; set; }
}
