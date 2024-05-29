using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TennisScoreboard.Contracts;
using TennisScoreboard.Models;
using TennisScoreboard.Services;

namespace TennisScoreboard.Controllers
{
    [ApiController]
    [Route("/match-score")]
    public class MatchScoreController : Controller
    {
        public Guid guidMatch;
        [HttpGet]
        public ActionResult GetMatch([FromQuery] string guid, IOngoingMatchesService matchesService, IPlayerSerise playerSerise)
        {
            guidMatch = Guid.Parse(guid);
            var matchScore = matchesService.GetMatchForDictionary(guidMatch);
            var namePlayer1 = playerSerise.GetPlayerById(matchScore.Player1Score.Id).Name;
            var namePlayer2 = playerSerise.GetPlayerById(matchScore.Player2Score.Id).Name;
            var score = new ScoreResponseBuilder(matchScore, namePlayer1, namePlayer2);
            return View("ScoreBoard", new MatchScoreContracts(score.PlayerScore1, score.PlayerScore2));
        }

        [HttpPost]
        public ActionResult AddPoint([FromQuery] Guid guid, [FromForm] int id,
            IPlayerSerise playerSerise,
            IOngoingMatchesService matchesService,
            IMatchScoreCalculationService calculationService, 
            IFinishedMatchesPersistenceService finishedService)
        {
            var currentMatchScore = matchesService.GetMatchForDictionary(guid);
            var matchScore = calculationService.AddPointForPlayer(currentMatchScore, id);
            var namePlayer1 = playerSerise.GetPlayerById(matchScore.Player1Score.Id).Name;
            var namePlayer2 = playerSerise.GetPlayerById(matchScore.Player2Score.Id).Name;
            if (matchScore.Player1Score.Set == 2 || matchScore.Player2Score.Set == 2)
            {
                matchScore.WinnerId = matchScore.Player1Score.Set == 2 ? matchScore.Player1Score.Id : matchScore.Player2Score.Id;
                var match = finishedService.SaveEndedMatch(matchScore, guidMatch);
                var winnerName = matchScore.WinnerId == matchScore.Player1Score.Id ? namePlayer1 : namePlayer2;
                return Ok(new MatchesResponse(match.Result.Id, namePlayer1, namePlayer2, winnerName));
            }
            var score = new ScoreResponseBuilder(currentMatchScore, namePlayer1, namePlayer2);
            return Ok(new MatchScoreContracts(score.PlayerScore1, score.PlayerScore2));
        }
    }
}
