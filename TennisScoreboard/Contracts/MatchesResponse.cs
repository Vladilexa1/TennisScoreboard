namespace TennisScoreboard.Contracts
{
    public record MatchesResponse(int id, string playerName1, string playerName2, string WinnerName)
    {
    }
}
