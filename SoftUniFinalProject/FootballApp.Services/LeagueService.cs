using FootballApp.Data;
using FootballApp.Data.Models;
using FootballApp.Services.Interfaces;
using FootballApp.ViewModels.League;
using Microsoft.EntityFrameworkCore;

namespace FootballApp.Services
{
    public class LeagueService : ILeagueService
    {
        private readonly FootballAppDbContext dbContext;

        public LeagueService(FootballAppDbContext context)
        {
            this.dbContext = context;
        }
        public async Task<List<AllLeaguesViewModel>> GetAllLeaguesAsync()
        {
            List<AllLeaguesViewModel> leagues = await dbContext.Leagues
                .Select(l => new AllLeaguesViewModel()
                {
                    Id = l.Id,
                    Logo = l.Logo,
                    Name = l.Name
                })
                .ToListAsync();

            return leagues;
        }

        public async Task<LeaguePageViewModel> GetLeagueByIdAsync(int leagueId)
        {
            var league = await dbContext.Leagues
                .Include(l=>l.Clubs)
                .FirstAsync(l=>l.Id == leagueId);

            var model = new LeaguePageViewModel()
            {
                Name = league.Name,
                Country = league.Country,
                Logo = league.Logo,
                Clubs = league.Clubs
                .Select(c => new LeagueClubViewModel()
                {
                    Id = c.Id,
                    Logo = c.Logo,
                    Name = c.Name,
                    GoalDifferrence = c.GoalDifference,
                    Points = c.Points,
                    Wins = c.Wins,
                    Draws = c.Draws,
                    Loses = c.Loses,
                    MathesPlayed = c.MatchesPlayed
                })
                .ToList()
            };
            return model;
        }
    }
}
