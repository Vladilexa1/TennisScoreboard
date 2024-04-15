using TennisScoreboard.Models;

namespace TennisScoreboard.Contracts
{
    public record MatchScoreResponse(string namePlayer1, string namePlayer2, MatchScoreContracts Score)
    {
    }
}
