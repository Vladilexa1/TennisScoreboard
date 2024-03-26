using TennisScoreboard.Data;
using TennisScoreboard.Models;

namespace TennisScoreboard.Services
{
    public class FinishedMatchesPersistenceService
    {
        private readonly IMatchRepository _matchRepository;
        public async Task<Match> SaveEndedMatch(Match match)
        {
            await _matchRepository.AddNewMatch(match);
            return match;
        }
    }
}
