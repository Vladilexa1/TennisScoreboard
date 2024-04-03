using Microsoft.AspNetCore.Mvc;
using TennisScoreboard.Contracts;
using TennisScoreboard.Models;
using TennisScoreboard.Services;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;

namespace TennisScoreboard.Controllers
{
    [ApiController]
    [Route("/new-match")]
    public class TennisScoreboardController : ControllerBase
    {
        [HttpPost]
        public ActionResult NewMatch([FromForm] NewMatchRequest matchRequest, 
            IOngoingMatchesService ongoingMatchesService, IPlayerSerise playerSerise)
        {
            var player1 = playerSerise.GetPlayerByName(matchRequest.playerName1);
            var player2 = playerSerise.GetPlayerByName(matchRequest.playerName2);
            var guidMatch = ongoingMatchesService.StartMatch(player1, player2); // redirect

            return Redirect($"/match-score?guid={guidMatch}");
        }
    }
}
