using Microsoft.EntityFrameworkCore;
using TennisScoreboard.Models;

namespace TennisScoreboard.Data
{
    public class TennisScoreboardRepository : IMatchRepository, IPlayerRepository
    {
        private readonly TennisScoreboardContext _dbContext;
        public TennisScoreboardRepository(TennisScoreboardContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<Player>> GetAllPlayer()
        {
            return await _dbContext.Players.ToListAsync();
        }
        public async Task<Player> GetPlayer(string name)
        {
            return await _dbContext.Players
                .Where(p => p.Name.Equals(name))
                .FirstOrDefaultAsync() 
                ?? throw new Exception();
        }
        public async Task<Player> GetPlayer(int id) // write castom exception
        {
            return await _dbContext.Players
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync()
                ?? throw new Exception("custom");
        }
        public async Task AddNewPlayer(Player player)
        {
            await _dbContext.Players
                .AddAsync(player);
            await _dbContext
                .SaveChangesAsync();
        }


        public async Task<List<Match>> GetAllMatches()
        {
            return await _dbContext.Matches.ToListAsync();
        }
        public async Task<Match> GetMatches(int id) // write castom exception
        {
            return await _dbContext.Matches
                .Where(m => m.Id == id)
                .FirstOrDefaultAsync()
                ?? throw new Exception("custom");
        }
        public async Task AddNewMatch(Match match)
        {
            await _dbContext.Matches.AddAsync(match);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<List<Match>> GetMatchByPage(int page, int pageSize)
        {
            return await _dbContext.Matches
                     .AsNoTracking()
                     .Skip((page - 1) * pageSize)
                     .Take(pageSize)
                     .ToListAsync();
        }

    }
}
