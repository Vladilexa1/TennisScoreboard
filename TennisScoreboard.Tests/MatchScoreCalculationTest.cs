using TennisScoreboard.Models;
using TennisScoreboard.Services;

namespace TennisScoreboard.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void AddPointByPlayer_MatchScoreAndId_Point1()
        {
            // arrange
            int id = 1;
            MatchScore matchScore = new MatchScore { Player1Score = new PlayerScore { Id = 1}, Player2Score = new PlayerScore {Id = 2 } };

            var expected = new MatchScore { Player1Score = new PlayerScore { Id = 1, Point = 1 }, Player2Score = new PlayerScore { Id = 2 } };

            
            MatchScoreCalculationService matchScoreCalculationService = new MatchScoreCalculationService();
            var actual =  matchScoreCalculationService.AddPointForPlayer(matchScore, id);

            Assert.AreEqual(expected.Player1Score.Point, actual.Player1Score.Point);
        }
        [Test]
        public void AddPointByPlayer_MatchScoreAndId_GameIsNotEnded()
        {
            // arrange
            int id = 1;
            MatchScore matchScore = new MatchScore { Player1Score = new PlayerScore { Id = 1, Point = 3 }, Player2Score = new PlayerScore { Id = 2, Point = 3 } };

            var expected = new MatchScore { Player1Score = new PlayerScore { Id = 1, Game = 0 }, Player2Score = new PlayerScore { Id = 2 } };


            MatchScoreCalculationService matchScoreCalculationService = new MatchScoreCalculationService();
            var actual = matchScoreCalculationService.AddPointForPlayer(matchScore, id);

            Assert.AreEqual(expected.Player1Score.Game, actual.Player1Score.Game);
        }
        [Test]
        public void AddPointByPlayer_MatchScoreAndId_GameIsEnded()
        {
            // arrange
            int id = 1;
            MatchScore matchScore = new MatchScore { Player1Score = new PlayerScore { Id = 1, Point = 3 }, Player2Score = new PlayerScore { Id = 2 } };

            var expected = new MatchScore { Player1Score = new PlayerScore { Id = 1, Game = 1 }, Player2Score = new PlayerScore { Id = 2 } };


            MatchScoreCalculationService matchScoreCalculationService = new MatchScoreCalculationService();
            var actual = matchScoreCalculationService.AddPointForPlayer(matchScore, id);

            Assert.AreEqual(expected.Player1Score.Game, actual.Player1Score.Game);
        }
        [Test]
        public void AddPointByPlayer_MatchScoreAndId_TiebreackStarted()
        {
            // arrange
            int id1 = 1;
            int id2 = 2;
            MatchScore matchScore = new MatchScore 
            { 
                Player1Score = new PlayerScore { Id = id1 },
                Player2Score = new PlayerScore { Id = id2 } 
            };

            var expected = true;


            MatchScoreCalculationService matchScoreCalculationService = new MatchScoreCalculationService();
            for (int i = 0; i < 20; i++)
            {
                matchScoreCalculationService.AddPointForPlayer(matchScore, id1);
            }
            for (int i = 0; i < 20; i++)
            {
                matchScoreCalculationService.AddPointForPlayer(matchScore, id2);
            }
            for (int i = 0; i < 4; i++)
            {
                matchScoreCalculationService.AddPointForPlayer(matchScore, id1);
            }
            for (int i = 0; i < 4; i++)
            {
                matchScoreCalculationService.AddPointForPlayer(matchScore, id2);
            }
            var actual = MatchScoreCalculationService.TieBreackIsStarted;

            Assert.AreEqual(expected, actual);
        }
    }
}