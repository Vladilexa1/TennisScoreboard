using Microsoft.EntityFrameworkCore;
using TennisScoreboard.Data;
using Microsoft.EntityFrameworkCore.Design;
using TennisScoreboard.Services;

namespace TennisScoreboard
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddDbContext<TennisScoreboardContext>(options =>
            {
                options.UseSqlite(builder.Configuration.GetConnectionString("SQLite"));
            });

            builder.Services.AddScoped<IMatchRepository, TennisScoreboardRepository>();
            builder.Services.AddScoped<IPlayerRepository, TennisScoreboardRepository>();
            builder.Services.AddScoped<IFinishedMatchesPersistenceService, FinishedMatchesPersistenceService>();
            builder.Services.AddScoped<IOngoingMatchesService, OngoingMatchesService>();
            builder.Services.AddScoped<IMatchScoreCalculationService, MatchScoreCalculationService>();
            builder.Services.AddScoped<IPlayerSerise, PlayerSerise>();
            builder.Services.AddScoped<IMatchesService, MatchesService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            using var scope = app.Services.CreateScope();
            
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
