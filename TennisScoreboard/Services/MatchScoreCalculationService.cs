using System.Reflection;
using TennisScoreboard.Data;
using TennisScoreboard.Models;

namespace TennisScoreboard.Services
{
    public class MatchScoreCalculationService : IMatchScoreCalculationService
    {
        private MatchScore matchScore;
        public static bool TieBreackIsStarted = false;
        public MatchScore AddPointForPlayer(MatchScore matchScore, int id)
        {
            var playerScore = matchScore.GetPlayerScoreForId(id);
            this.matchScore = matchScore;
            AddPoint(playerScore);
            return matchScore;
        }
        private void AddPoint(PlayerScore playerScore)
        {
            if (TieBreackIsStarted)
            {
                Tiebreak(playerScore);
                return;
            }
            playerScore.Point += 1; 
            if (playerScore.Point >= 4 && Math.Abs(matchScore.Player1Score.Point - matchScore.Player2Score.Point) >= 2)
            {
                playerScore.Game += 1;
                ResetAllPoint();
            }
            if (playerScore.Game == 6 && Math.Abs(matchScore.Player1Score.Game - matchScore.Player2Score.Game) >= 2)
            {
                AddSet(playerScore);
            }
            if (matchScore.Player1Score.Game == 6 && matchScore.Player2Score.Game == 6)
            {
                TieBreackIsStarted = true;
                ResetAllPoint();
            }
        }
        private void Tiebreak(PlayerScore playerScore)
        {
            playerScore.Point += 1;
            if (playerScore.Point == 7)
            {
                AddSet(playerScore);
                TieBreackIsStarted = false;
            }
        }
        private void AddSet(PlayerScore playerScore)
        {
            playerScore.Set += 1;
            ResetAllPoint();
            ResetAllGame();
        }
        private void ResetAllPoint()
        {
            matchScore.Player1Score.Point = 0;
            matchScore.Player2Score.Point = 0;
        }
        private void ResetAllGame()
        {
            matchScore.Player1Score.Game = 0;
            matchScore.Player2Score.Game = 0;
        }
    }
}
