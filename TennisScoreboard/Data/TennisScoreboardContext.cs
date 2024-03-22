using Microsoft.EntityFrameworkCore;
using TennisScoreboard.Models;

namespace TennisScoreboard.Data
{
    public class TennisScoreboardContext(DbContextOptions<TennisScoreboardContext> options) : DbContext(options)
    {
        public DbSet<Player> Players { get; set; }
        public DbSet<Match> Matches { get; set; }

    }
}
