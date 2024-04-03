using TennisScoreboard.Models;

namespace TennisScoreboard.Services
{
    public interface IOngoingMatchesService
    {
        Guid StartMatch(Player player1, Player player2);
        MatchScore GetMatchForDictionary(Guid guid);
    }
}