

using TennisScoreboard.Models;

namespace TennisScoreboard.Contracts
{
    public record MatchScoreContracts(PlayerScoreContracts PlayerScore1, PlayerScoreContracts PlayerScore2)
    {
    }
}
