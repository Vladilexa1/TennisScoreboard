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
            return Ok(new MatchScoreResponse(namePlayer1, namePlayer2, currentMatchScore));
        }
        [HttpPost("{id}")]
        public ActionResult AddPoint(int id,
            IMatchScoreCalculationService calculationService, 
            IFinishedMatchesPersistenceService finishedService)
        {
            var matchScore = calculationService.AddPointForPlayer(currentMatchScore, id);
            currentMatchScore = matchScore;
            if (currentMatchScore.Player1Score.Set == 2 || currentMatchScore.Player2Score.Set == 2)
            {
                finishedService.SaveEndedMatch(currentMatchScore);
                currentMatchScore = null;
            }

            return Ok(matchScore);
        } 
    }
}
