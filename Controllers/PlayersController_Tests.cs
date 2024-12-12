using Moq;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using TennisPlayersStats.Models;
using TennisPlayersStats.Services;

namespace TennisPlayersStats.Controllers;

public class PlayersController_Tests
{
	private readonly Mock<IPlayerService> _mockPlayerService;
	private readonly PlayersController _controller;

	public PlayersController_Tests()
	{
		_mockPlayerService = new Mock<IPlayerService>();

		_controller = new PlayersController(_mockPlayerService.Object);
	}

	[Fact]
	public void GetPlayers_ShouldReturnListOfPlayers()
	{
		var mockPlayers = new List<Player>
		{
			new() { Id = 52, FirstName = "Novak", LastName = "Djokovic", Data = new PlayerData { Rank = 2 } },
			new() { Id = 95, FirstName = "Venus", LastName = "Williams", Data = new PlayerData { Rank = 52 } }
		};
		_mockPlayerService.Setup(service => service.GetRankedPlayers()).Returns(mockPlayers);

		var result = _controller.GetRankedPlayers();

		var okResult = Assert.IsType<OkObjectResult>(result);
		var players = Assert.IsType<List<Player>>(okResult.Value);
		Assert.Equal(2, players.Count);
		Assert.Equal("Novak", players[0].FirstName);
		Assert.Equal("Venus", players[1].FirstName);
	}

	[Fact]
	public void GetPlayerById_ShouldReturnPlayer_WhenValidId()
	{
		var mockPlayer = new Player { Id = 52, FirstName = "Novak", LastName = "Djokovic" };
		_mockPlayerService.Setup(service => service.GetPlayerById(52)).Returns(mockPlayer);

		var result = _controller.GetPlayerById(52);

		var okResult = Assert.IsType<OkObjectResult>(result);
		var player = Assert.IsType<Player>(okResult.Value);
		Assert.Equal(52, player.Id);
		Assert.Equal("Novak", player.FirstName);
	}

	[Fact]
	public void GetPlayerById_ShouldReturnNotFound_WhenInvalidId()
	{
		_mockPlayerService.Setup(service => service.GetPlayerById(It.IsAny<int>())).Returns((Player)null);

		var result = _controller.GetPlayerById(999);

		Assert.IsType<NotFoundResult>(result);
	}

	[Fact]
	public void GetStats_ShouldReturnValidStats()
	{
		var country = new Country { Code = "USA" };
		var mockStats = new StatsResult
		{
			CountryWithMostWins = country,
			PlayersAverageBMI = 23.4f,
			PlayersMedianHeight = 180
		};
		_mockPlayerService.Setup(service => service.GetStats()).Returns(mockStats);

		var result = _controller.GetStats();

		var okResult = Assert.IsType<OkObjectResult>(result);
		var stats = Assert.IsType<StatsResult>(okResult.Value);
		Assert.Equal(country, stats.CountryWithMostWins);
		Assert.True(stats.PlayersAverageBMI > 0);
		Assert.True(stats.PlayersMedianHeight > 0);
	}
}