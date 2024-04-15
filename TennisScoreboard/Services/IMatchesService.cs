using TennisScoreboard.Models;

namespace TennisScoreboard.Services
{
    public interface IMatchesService
    {
        public List<Match> GetAllMatch();
        public List<Match> GetMatchByPage(int page, int pageSize);
        public List<Match> GetMatchByPageForPlayerName(int page, int pageSize, string playerName);
    }
}
