using Newtonsoft.Json;

namespace TennisPlayersStats;

public class PlayerService(IConfiguration configuration)
{
	private Root? _cachedPlayers;
	private DateTime _cacheExpiration = DateTime.MinValue;

	public Root LoadPlayers()
    {
        try
        {
            if (_cachedPlayers == null || DateTime.UtcNow > _cacheExpiration)
			{
				string jsonString = File.ReadAllText(configuration["PlayersFilePath"]);
				_cachedPlayers = JsonConvert.DeserializeObject<Root>(jsonString);
				_cacheExpiration = DateTime.UtcNow.AddMinutes(5);
			}
			return _cachedPlayers;
        }
        catch (Exception ex)
        {
            throw new Exception("Erreur lors du chargement du fichier JSON", ex);
        }
    }

	public List<Player> GetRankedPlayers()
	{
		var players = LoadPlayers();
		return players.Players.OrderBy(p => p.Data.Rank).ToList();
	}

    public Country GetCountryWithMostWins()
    {
        var players = LoadPlayers();

		return players.Players
				.GroupBy(player => player.Country)
				.OrderByDescending(group => group.Count())
				.Select(group => group.Key)
				.FirstOrDefault();
	}

    public float GetAveragePlayersBMI()
    {
		var players = LoadPlayers();
        var totalWeight = 0f;
        players.Players.ForEach(player => totalWeight += player.Data.Weight);
        return totalWeight / players.Players.Count;
	}

    public int GetPlayersMedianHeight()
    {
		var players = LoadPlayers();
        var playersHeights = players.Players.Select(player => player.Data.Height).ToList();
        return playersHeights.GetMedian();
	}
}