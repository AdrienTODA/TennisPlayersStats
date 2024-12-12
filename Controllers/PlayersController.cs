using Microsoft.AspNetCore.Mvc;
using TennisPlayersStats.Services;

namespace TennisPlayersStats.Controllers;

[ApiController]
[Route("[controller]")]
public class PlayersController(IPlayerService playerService) : ControllerBase
{
	[HttpGet("getplayersbyrank")]
	public IActionResult GetRankedPlayers()
	{
		var rankedPlayers = playerService.GetRankedPlayers();
		return Ok(rankedPlayers);
	}

    [HttpGet("{id}")]
    public IActionResult GetPlayerById(int id)
    {
        try
        {
            var player = playerService.GetPlayerById(id);
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

    [HttpGet("getstats")]
    public IActionResult GetStats()
    {
        var stats = playerService.GetStats();
        return Ok(stats);
    }
}