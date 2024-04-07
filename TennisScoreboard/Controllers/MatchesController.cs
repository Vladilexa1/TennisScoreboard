using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using TennisScoreboard.Services;

namespace TennisScoreboard.Controllers
{
    [ApiController]
    [Route("/matches")]
    public class MatchesController : ControllerBase
    {
        [HttpGet]
        public ActionResult GetEndedMatches(IMatchesService matches, [FromQuery] string playerName, [FromQuery] int page = 1)
        {
            return Ok();
        }
    }
}
