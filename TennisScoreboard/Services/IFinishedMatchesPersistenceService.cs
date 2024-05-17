using TennisScoreboard.Models;

namespace TennisScoreboard.Services
{
    public interface IFinishedMatchesPersistenceService
    {
        Task<Match> SaveEndedMatch(MatchScore matchScore, Guid guid);
    }
}