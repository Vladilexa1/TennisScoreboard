namespace TennisScoreboard.Models
{
    public class MatchScore
    {
        public PlayerScore Player1Score { get; set; }
        public PlayerScore Player2Score { get; set; }
        public int WinnerId { get; set; }
        public PlayerScore GetPlayerScoreForId(int id)
        {
            if (Player1Score.Id == id) return Player1Score;
            else return Player2Score;
        }
    }
}
