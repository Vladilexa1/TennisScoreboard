using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TennisScoreboard.Models;

namespace TennisScoreboard.Configuration
{
    public class MatchConfiguration : IEntityTypeConfiguration<Match>
    {
        public void Configure(EntityTypeBuilder<Match> builder)
        {
            builder
                .HasKey(m => m.Id);
            builder
                .HasIndex(p => new { p.Player1Id, p.Player2Id });
            builder
                .HasOne(m => m.Player1)
                .WithMany(p => p.MatchesAsPlayer1)
                .HasForeignKey(m => m.Player1Id)
                .OnDelete(DeleteBehavior.Restrict);
            builder
                .HasOne(m => m.Player2)
                .WithMany(p => p.MatchesAsPlayer2)
                .HasForeignKey(m => m.Player2Id)
                .OnDelete(DeleteBehavior.Restrict);
            builder
                .HasOne(m => m.Winner)
                .WithMany(p => p.WinMatches)
                .HasForeignKey(m => m.WinnerId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
