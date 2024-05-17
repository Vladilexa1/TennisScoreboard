using Microsoft.AspNetCore.Mvc;
using TennisScoreboard.Contracts;
using TennisScoreboard.Models;
using TennisScoreboard.Services;

namespace TennisScoreboard.Controllers
{
    [ApiController]
    [Route("/match-score")]
    public class MatchScoreController : Controller
    {
        public static MatchScore currentMatchScore;
        public static Guid guidMatch;
        [HttpGet]
        public ActionResult GetMatch([FromQuery] string guid, IOngoingMatchesService matchesService, IPlayerSerise playerSerise)
        {
            guidMatch = Guid.Parse(guid);
            currentMatchScore = matchesService.GetMatchForDictionary(guidMatch);
            var namePlayer1 = playerSerise.GetPlayerById(currentMatchScore.Player1Score.Id).Name;
            var namePlayer2 = playerSerise.GetPlayerById(currentMatchScore.Player2Score.Id).Name;
            var score = new ScoreResponseBuilder(currentMatchScore, namePlayer1, namePlayer2);

            return View("ScoreBoard", new MatchScoreContracts(score.PlayerScore1, score.PlayerScore2));//Ok();
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
                var match = finishedService.SaveEndedMatch(currentMatchScore, guidMatch);
                var winnerName = currentMatchScore.WinnerId == currentMatchScore.Player1Score.Id ? namePlayer1 : namePlayer2;
                currentMatchScore = null;
                return Ok(new MatchesResponse(match.Result.Id, namePlayer1, namePlayer2, winnerName));
            }
            var score = new ScoreResponseBuilder(currentMatchScore, namePlayer1, namePlayer2);
            return Ok(new MatchScoreContracts(score.PlayerScore1, score.PlayerScore2));
        }
    }
}
