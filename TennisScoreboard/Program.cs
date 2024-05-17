using Microsoft.EntityFrameworkCore;
using TennisScoreboard.Data;
using TennisScoreboard.Services;

namespace TennisScoreboard
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<TennisScoreboardContext>(options =>
            {
                options.UseSqlite(builder.Configuration.GetConnectionString("SQLite"));
            });

            RegistryService(builder);

            var app = builder.Build();

            using var scope = app.Services.CreateScope();
            
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthorization();

            app.MapControllerRoute(
              name: "default",
              pattern: "{controller=HomePage}/{action=StartView}");

            app.Run();
        }
        public static void RegistryService(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IMatchRepository, TennisScoreboardRepository>();
            builder.Services.AddScoped<IPlayerRepository, TennisScoreboardRepository>();
            builder.Services.AddScoped<IFinishedMatchesPersistenceService, FinishedMatchesPersistenceService>();
            builder.Services.AddScoped<IOngoingMatchesService, OngoingMatchesService>();
            builder.Services.AddScoped<IMatchScoreCalculationService, MatchScoreCalculationService>();
            builder.Services.AddScoped<IPlayerSerise, PlayerSerise>();
            builder.Services.AddScoped<IMatchesService, MatchesService>();
        }
    }
}
