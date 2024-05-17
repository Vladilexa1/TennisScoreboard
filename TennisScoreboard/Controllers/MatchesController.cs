using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration.UserSecrets;
using TennisScoreboard.Contracts;
using TennisScoreboard.Models;
using TennisScoreboard.Services;

namespace TennisScoreboard.Controllers
{
    [ApiController]
    [Route("/matches")]
    public class MatchesController : Controller
    {
        public const int PAGE_SIZE = 5;
        [HttpGet]
        public ActionResult GetEndedMatches(IMatchesService matchService, IPlayerSerise playerSerise, [FromQuery] string playerName = "", [FromQuery] int page = 1)
        {
            List<Match> matches = new();
            List<MatchesResponse> result = new();
            try
            {
                if (!String.IsNullOrEmpty(playerName))
                {
                    matches = matchService.GetMatchByPageForPlayerName(page, PAGE_SIZE, playerName);
                }
                else
                {
                    matches = matchService.GetMatchByPage(page, PAGE_SIZE);
                }
            }
            catch (Exception)
            {
                return StatusCode(404);
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
            if (result.Count == 0)
            {
                return StatusCode(404); // TODO: изменить статус код
            }
            if (page < 1)
            {
                return StatusCode(404);
            }
            return View("Matches", result);
        }
    }
}
