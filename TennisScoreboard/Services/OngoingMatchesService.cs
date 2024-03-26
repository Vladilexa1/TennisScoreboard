using TennisScoreboard.Models;

namespace TennisScoreboard.Services
{
    public class OngoingMatchesService
    {
        public MatchScore? MatchScore { get; private set; }
        
        public MatchScore StartMatch()
        {
            MatchScore = new MatchScore();
            MatchScore.Player1Score = new PlayerScore();
            MatchScore.Player2Score = new PlayerScore();
            return MatchScore;
        }
        //public MatchScore GetMatchScore() // custom Exception
        //{
        //    return MatchScore ?? throw new Exception();
        //}
    }
}
