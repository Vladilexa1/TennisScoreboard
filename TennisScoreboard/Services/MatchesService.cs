using TennisScoreboard.Data;
using TennisScoreboard.Models;

namespace TennisScoreboard.Services
{
    public class MatchesService : IMatchesService
    {
        private readonly IMatchRepository _matchRepository;
        public MatchesService(IMatchRepository matchRepository)
        {
            _matchRepository = matchRepository;
        }
        public List<Match> GetAllMatch()
        {
            return _matchRepository.GetAllMatches().Result;
        }
        public List<Match> GetMatchByPage(int page, int pageSize)
        {
            return _matchRepository.GetMatchByPage(page, pageSize).Result;
        }
    }
}
