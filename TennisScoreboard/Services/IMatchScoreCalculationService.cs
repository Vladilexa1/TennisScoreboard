using TennisScoreboard.Models;

namespace TennisScoreboard.Services
{
    public interface IMatchScoreCalculationService
    {
        MatchScore AddPointForPlayer(MatchScore matchScore, int id);
    }
}