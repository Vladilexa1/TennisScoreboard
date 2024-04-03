using System.Reflection;
using TennisScoreboard.Data;
using TennisScoreboard.Models;

namespace TennisScoreboard.Services
{
    public class MatchScoreCalculationService : IMatchScoreCalculationService
    {
        private MatchScore matchScore;
        private bool TieBreackIsStarted = false;
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
            }
            playerScore.Point += 1; // TODO: возможен конфликт
            if (playerScore.Point >= 4 && Math.Abs(matchScore.Player1Score.Point - matchScore.Player2Score.Point) >= 2)
            {
                playerScore.Game += 1;
                ResetAllPoint();
            }
            if (matchScore.Player1Score.Game == 6 && matchScore.Player2Score.Game == 6)
            {
                TieBreackIsStarted = true;
                ResetAllPoint();
            }
            if (matchScore.Player1Score.Game == 6 || matchScore.Player2Score.Game == 6)
            {
                AddSet(playerScore);
            }
        }
        private void Tiebreak(PlayerScore playerScore)
        {
            playerScore.Point += 1;
            if (playerScore.Point == 7)
            {
                AddSet(playerScore);
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
