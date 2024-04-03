using TennisScoreboard.Models;

namespace TennisScoreboard.Services
{
    public interface IPlayerSerise
    {
        Player GetPlayerByName(string playerName);
        Player GetPlayerById(int id);
    }
}