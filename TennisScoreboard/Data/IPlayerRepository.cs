using TennisScoreboard.Models;

namespace TennisScoreboard.Data
{
    public interface IPlayerRepository
    {
        Task<Player> GetPlayer(int id);
        Task<Player> GetPlayer(string name);
        Task<List<Player>> GetAllPlayer();
        Task AddNewPlayer(Player player);
    }
}
