using System.Reflection;
using TennisScoreboard.Models;

namespace TennisScoreboard.Services
{
    public class MatchScoreCalculationService
    {
        private MatchScore matchScore;
        private bool TieBreackIsStarted = false;
        private bool IsWinnerDeterminate = false;
        public MatchScoreCalculationService(MatchScore matchScore)
        {
            this.matchScore = matchScore;
        }
        public MatchScore AddPointForPlayer(Player player)
        {
            var playerScore = matchScore.GetPlayerScoreForId(player.Id);
            AddPoint(playerScore);
            if (IsWinnerDeterminate)
            {
                Match match = new Match
                {
                    Player1Id = matchScore.Player1Score.Id,
                    Player2Id = matchScore.Player2Score.Id,
                    WinnerId = matchScore.WinnerId
                };
                EndMatch(match);
            }
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
            if (playerScore.Set == 2)
            {
                matchScore.WinnerId = playerScore.Id;
                IsWinnerDeterminate = true;
            }
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
        private async void EndMatch(Match match)
        {
            FinishedMatchesPersistenceService finishedMatchesPersistenceService = new FinishedMatchesPersistenceService();
            await finishedMatchesPersistenceService.SaveEndedMatch(match);
        }
    }
}
