using TennisScoreboard.Models;

namespace TennisScoreboard.Services
{
    public interface IMatchesService
    {
        public List<Match> GetAllMatch();
        public List<Match> GetMatchByPage(int page, int pageSize);
    }
}
