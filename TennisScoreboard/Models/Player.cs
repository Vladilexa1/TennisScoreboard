namespace TennisScoreboard.Models
{
    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<Match>? MatchesAsPlayer1 { get; set; }
        public List<Match>? MatchesAsPlayer2 { get; set; }
        public List<Match>? WinMatches { get; set; }
    }
}
