using TennisScoreboard.Contracts;
using TennisScoreboard.Models;

namespace TennisScoreboard.Services
{
    public class ScoreResponseBuilder
    {
        public ScoreResponseBuilder(MatchScore score, string namePlayer1, string namePlayer2)
        {
            PlayerScore1 = new PlayerScoreContracts
            {
                Id = score.Player1Score.Id,
                Name = namePlayer1,
                Game = score.Player1Score.Game,
                Set = score.Player1Score.Set
            };
            PlayerScore2 = new PlayerScoreContracts
            {
                Id = score.Player2Score.Id,
                Name = namePlayer2,
                Game = score.Player2Score.Game,
                Set = score.Player2Score.Set
            };
            if (!MatchScoreCalculationService.TieBreackIsStarted)
            {
                ConvertPoint(score);
            }
            else
            {
                ConvertPointTieBreack(score);
            }
        }
        private void ConvertPointTieBreack(MatchScore score)
        {
            PlayerScore1.Point = score.Player1Score.Point.ToString();
            PlayerScore2.Point = score.Player2Score.Point.ToString();
        }
        public PlayerScoreContracts PlayerScore1 { get; set; }
        public PlayerScoreContracts PlayerScore2 { get; set; }
        private void ConvertPoint(MatchScore matchScore)
        {
            var point1 = matchScore.Player1Score.Point;
            var point2 = matchScore.Player2Score.Point;

            PlayerScore1.Point = PointToString(point1);
            PlayerScore2.Point = PointToString(point2);

            if (point1 > 3 || point2 > 3)
            {
                if (point1 > point2)
                {
                    PlayerScore1.Point = "ad";
                    PlayerScore2.Point = "40";
                }
                else if (point1 < point2)
                {
                    PlayerScore1.Point = "40";
                    PlayerScore2.Point = "ad";
                }
                else if (point1 == point2)
                {
                    PlayerScore1.Point = "40";
                    PlayerScore2.Point = "40";
                }
            }
        }
        private string PointToString(int point)
        {
            return point switch
            {
                0 => "0",
                1 => "15",
                2 => "30",
                3 => "40",
                _ => "ad"
            };
        }
    }
}

