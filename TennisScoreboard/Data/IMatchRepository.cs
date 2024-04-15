using TennisScoreboard.Models;

namespace TennisScoreboard.Data
{
    public interface IMatchRepository
    {
        Task AddNewMatch(Match match);
        Task<List<Match>> GetAllMatches();
        Task<List<Match>> GetMatchByPage(int page, int pageSize);
        Task<List<Match>> GetMatchByPageForPlayerName(int page, int pageSize, string playerName);
        Task<Match> GetMatches(int id);
    }
}