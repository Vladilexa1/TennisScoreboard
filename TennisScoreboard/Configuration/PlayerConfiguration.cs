using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TennisScoreboard.Models;

namespace TennisScoreboard.Configuration
{
    public class PlayerConfiguration : IEntityTypeConfiguration<Player>
    {
        public void Configure(EntityTypeBuilder<Player> builder)
        {
            builder
                .HasKey(x => x.Id);
            builder
                .HasIndex(x => x.Name);
            builder
                .HasMany(p => p.MatchesAsPlayer1)
                .WithOne(m => m.Player1)
                .HasForeignKey(m => m.Player1Id);
            builder
                .HasMany(p => p.MatchesAsPlayer2)
                .WithOne(m => m.Player2)
                .HasForeignKey(m => m.Player2Id);
            builder
                .HasMany(p => p.WinMatches)
                .WithOne(m => m.Winner)
                .HasForeignKey(m => m.WinnerId);
        }
    }
}
