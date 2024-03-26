using TennisScoreboard.Models;

namespace TennisScoreboard.Data
{
    public interface IPlayerRepository
    {
        Task<Player> GetPlayer(int id);
        Task<List<Player>> GetAllPlayer();
        Task AddNewPlayer(Player player);
    }
}
