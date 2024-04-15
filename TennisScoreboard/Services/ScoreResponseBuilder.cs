using TennisScoreboard.Contracts;
using TennisScoreboard.Models;

namespace TennisScoreboard.Services
{
    public class ScoreResponseBuilder
    {
        public ScoreResponseBuilder(MatchScore score)
        {
            PlayerScore1 = new PlayerScoreContracts
            {
                Id = score.Player1Score.Id,
                Game = score.Player1Score.Game,
                Set = score.Player1Score.Set
            };
            PlayerScore2 = new PlayerScoreContracts
            {
                Id = score.Player2Score.Id,
                Game = score.Player2Score.Game,
                Set = score.Player2Score.Set
            };
            ConvertPoint(score);
        }
        public PlayerScoreContracts PlayerScore1 { get; set; }
        public PlayerScoreContracts PlayerScore2 { get; set; }
        
        public void ConvertPoint(MatchScore matchScore) // TODO: Переписать, DRY
        {
            var point1 = matchScore.Player1Score.Point;
            var point2 = matchScore.Player2Score.Point;
            switch (point1)
            {
                case 0:
                    PlayerScore1.Point = "0";
                    break;
                case 1:
                    PlayerScore1.Point = "15";
                    break;
                case 2:
                    PlayerScore1.Point = "30";
                    break;
                case 3:
                    PlayerScore1.Point = "40";
                    break;
                case > 3:
                    if (point1 > point2)
                    {
                        PlayerScore1.Point = "ad";
                        PlayerScore2.Point = "";
                        return;
                    }
                    else
                    {
                        PlayerScore1.Point = "";
                    }
                    break;
            }
            switch (point2)
            {
                case 0:
                    PlayerScore2.Point = "0";
                    break;
                case 1:
                    PlayerScore2.Point = "15";
                    break;
                case 2:
                    PlayerScore2.Point = "30";
                    break;
                case 3:
                    PlayerScore2.Point = "40";
                    break;
                case > 3:
                    if (point1 < point2)
                    {
                        PlayerScore2.Point = "ad";
                        PlayerScore1.Point = "";
                        return;
                    }
                    else
                    {
                        PlayerScore2.Point = "";
                    }
                    break;
            }
        }
    }
}

