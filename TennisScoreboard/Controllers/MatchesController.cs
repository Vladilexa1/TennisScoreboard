using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;
using TennisScoreboard.Contracts;
using TennisScoreboard.Models;
using TennisScoreboard.Services;

namespace TennisScoreboard.Controllers
{
    [ApiController]
    [Route("/matches")]
    public class MatchesController : ControllerBase
    {
        public const int PAGE_SIZE = 5;
        [HttpGet]
        public ActionResult GetEndedMatches(IMatchesService matchService, IPlayerSerise playerSerise, [FromQuery] string playerName = "", [FromQuery] int page = 1)
        {
            List<Match> matches = new();
            List<MatchesResponse> result = new();
            if (!String.IsNullOrEmpty(playerName))
            {
                 matches = matchService.GetMatchByPageForPlayerName(page, PAGE_SIZE, playerName);
            }
            else
            {
                matches = matchService.GetMatchByPage(page, PAGE_SIZE);
            }
            foreach (var item in matches)
            {
                result.Add(
                    new MatchesResponse
                    (
                        item.Id, 
                        playerSerise.GetPlayerById(item.Player1Id).Name, 
                        playerSerise.GetPlayerById(item.Player2Id).Name, 
                        playerSerise.GetPlayerById(item.WinnerId).Name)
                    );
            }
            return Ok(result);
        }
    }
}
