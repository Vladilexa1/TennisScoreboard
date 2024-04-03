using TennisScoreboard.Models;

namespace TennisScoreboard.Services
{
    public class OngoingMatchesService : IOngoingMatchesService
    {
        private static Dictionary<Guid, MatchScore> matches = new Dictionary<Guid, MatchScore>();
        private bool MatchIsStarted = false;
        public Guid StartMatch(Player player1, Player player2) // see exception
        { 
            if (!MatchIsStarted)
            {
                MatchIsStarted = true;
                var matchScore = new MatchScore();
                matchScore.Player1Score = new PlayerScore{ Id = player1.Id};
                matchScore.Player2Score = new PlayerScore { Id = player2.Id };
                var guid = Guid.NewGuid();
                matches.Add(guid, matchScore );
                return guid;
            }
            else
            {
                throw new Exception();
            }
        }
        public MatchScore GetMatchForDictionary(Guid guid)
        {
            matches.TryGetValue(guid, out MatchScore matchScore);
            return matchScore ?? throw new ArgumentException();
        }
    }
}
