using Microsoft.AspNetCore.Mvc;

namespace TennisPlayersStats;

[ApiController]
[Route("[controller]")]
public class PlayersController(PlayerService playerService) : ControllerBase
{
	[HttpGet]
    public IActionResult GetAllPlayers()
    {
        try
        {
            var players = playerService.LoadPlayers();
            return Ok(players);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet("{id}")]
    public IActionResult GetPlayerById(int id)
    {
        try
        {
            var players = playerService.LoadPlayers();
            var player = players.Players.FirstOrDefault(p => p.Id == id);
            if (player == null)
            {
                return NotFound();
            }
            return Ok(player);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

	[HttpGet("ranked")]
	public IActionResult GetRankedPlayers()
	{
		var rankedPlayers = playerService.GetRankedPlayers();
		return Ok(rankedPlayers);
	}

	[HttpGet("countrywithmostwins")]
	public IActionResult GetCountryWithMostWins()
	{
		var countryWithMostWins = playerService.GetCountryWithMostWins();
		return Ok(countryWithMostWins);
	}

	[HttpGet("averagebmi")]
    public IActionResult GetPlayersAverageBMI()
    {
        var averagePlayersBMI = playerService.GetAveragePlayersBMI();
        return Ok(averagePlayersBMI);
    }

	[HttpGet("medianheight")]
	public IActionResult GetPlayersMedianHeight()
	{
		var medianPlayersHeight = playerService.GetPlayersMedianHeight();
		return Ok(medianPlayersHeight);
	}
}