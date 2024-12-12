using Newtonsoft.Json;
using Xunit;
using Moq;
using TennisPlayersStats.Models;

namespace TennisPlayersStats.Services;

public class PlayerService_Tests
{
	private readonly PlayerService _playerService;
	private readonly Mock<IConfiguration> _mockConfiguration;

	public PlayerService_Tests()
	{
		_mockConfiguration = new Mock<IConfiguration>();
		_mockConfiguration.Setup(config => config["PlayersFilePath"]).Returns("Resources/headtohead.json");
		var jsonData = File.ReadAllText("Resources/headtohead.json");
		var playersData = JsonConvert.DeserializeObject<Root>(jsonData);
		_playerService = new(_mockConfiguration.Object);
	}

	[Fact]
	public void GetRankedPlayers_ShouldReturnAllPlayers()
	{
		var players = _playerService.GetRankedPlayers().ToList();

		Assert.NotNull(players);
		Assert.Equal(5, players.Count);
	}

	[Fact]
	public void GetSortedPlayers_ShouldReturnPlayersSortedByRankDesc()
	{
		var players = _playerService.GetRankedPlayers().ToList();

		Assert.Equal(1, players[0].Data.Rank);
	}

	[Fact]
	public void GetPlayerById_ShouldReturnCorrectPlayer_WhenValidId()
	{
		var player = _playerService.GetPlayerById(52);

		Assert.NotNull(player);
		Assert.Equal(52, player.Id);
		Assert.Equal("N.DJO", player.ShortName);
	}

	[Fact]
	public void GetPlayerById_ShouldReturnNull_WhenInvalidId()
	{
		var player = _playerService.GetPlayerById(999);

		Assert.Null(player);
	}

	[Fact]
	public void GetStats_ShouldReturnValidStats()
	{
		var stats = _playerService.GetStats();

		Assert.NotNull(stats);
		Assert.NotNull(stats.CountryWithMostWins);
		Assert.True(stats.PlayersAverageBMI > 0);
		Assert.True(stats.PlayersMedianHeight > 0);
	}
}