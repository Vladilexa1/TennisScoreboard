using Microsoft.AspNetCore.Mvc.ModelBinding;
using TennisScoreboard.Data;
using TennisScoreboard.Models;

namespace TennisScoreboard.Services
{
    public class FinishedMatchesPersistenceService : IFinishedMatchesPersistenceService
    {
        private readonly IMatchRepository _matchRepository;
        public FinishedMatchesPersistenceService(IMatchRepository matchRepository)
        {
            _matchRepository = matchRepository;
        }
        public async Task<Match> SaveEndedMatch(MatchScore matchScore)
        {
            Match match = new Match
            {
                Player1Id = matchScore.Player1Score.Id,
                Player2Id = matchScore.Player2Score.Id,
                WinnerId = matchScore.WinnerId
            };
            await _matchRepository.AddNewMatch(match);
            return match;
        }
        
    }
}

