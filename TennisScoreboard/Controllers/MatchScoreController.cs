using Microsoft.AspNetCore.Mvc;
using TennisScoreboard.Contracts;
using TennisScoreboard.Models;
using TennisScoreboard.Services;

namespace TennisScoreboard.Controllers
{
    [ApiController]
    [Route("/match-score")]
    public class MatchScoreController : ControllerBase
    {
        public static MatchScore currentMatchScore;
        [HttpGet]
        public ActionResult GetMatch([FromQuery] string guid, IOngoingMatchesService matchesService, IPlayerSerise playerSerise)
        {
            var guidMatch = Guid.Parse(guid);
            currentMatchScore = matchesService.GetMatchForDictionary(guidMatch);
            var namePlayer1 = playerSerise.GetPlayerById(currentMatchScore.Player1Score.Id).Name;
            var namePlayer2 = playerSerise.GetPlayerById(currentMatchScore.Player2Score.Id).Name;
            var score = new ScoreResponseBuilder(currentMatchScore);
            
            return Ok(new MatchScoreResponse(namePlayer1, namePlayer2, new MatchScoreContracts(score.PlayerScore1, score.PlayerScore2)));
        }
        [HttpPost("{id}")]
        public ActionResult AddPoint(int id,
            IMatchScoreCalculationService calculationService, 
            IFinishedMatchesPersistenceService finishedService,
            IPlayerSerise playerSerise)
        {
            var matchScore = calculationService.AddPointForPlayer(currentMatchScore, id);
            currentMatchScore = matchScore;
            var namePlayer1 = playerSerise.GetPlayerById(currentMatchScore.Player1Score.Id).Name;
            var namePlayer2 = playerSerise.GetPlayerById(currentMatchScore.Player2Score.Id).Name;
            if (currentMatchScore.Player1Score.Set == 2 || currentMatchScore.Player2Score.Set == 2)
            {
                currentMatchScore.WinnerId = currentMatchScore.Player1Score.Set == 2 ? currentMatchScore.Player1Score.Id : currentMatchScore.Player2Score.Id;
                var match = finishedService.SaveEndedMatch(currentMatchScore);
                var winnerName = currentMatchScore.WinnerId == currentMatchScore.Player1Score.Id ? namePlayer1 : namePlayer2;
                currentMatchScore = null;
                return Ok(new MatchesResponse(match.Result.Id, namePlayer1, namePlayer2, winnerName));
            }
           
            var score = new ScoreResponseBuilder(currentMatchScore);
            return Ok(new MatchScoreResponse(namePlayer1, namePlayer2, new MatchScoreContracts(score.PlayerScore1, score.PlayerScore2)));
        }
       
        
        public MatchScore test()
        {
            return new MatchScore { Player1Score = new PlayerScore { Id = 3, Game = 5, Point = 3, Set = 1 }, Player2Score = new PlayerScore { Id = 4, Game = 0, Point = 0, Set = 1 } };
        }
    }
}
