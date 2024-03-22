using Microsoft.EntityFrameworkCore;
using TennisScoreboard.Configuration;
using TennisScoreboard.Models;

namespace TennisScoreboard.Data
{
    public class TennisScoreboardContext(DbContextOptions<TennisScoreboardContext> options) : DbContext(options)
    {
        public DbSet<Player> Players { get; set; }
        public DbSet<Match> Matches { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PlayerConfiguration());
            modelBuilder.ApplyConfiguration(new MatchConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
