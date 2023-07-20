using FootballApp.Data;
using FootballApp.Data.Models;
using FootballApp.Services.Interfaces;
using FootballApp.ViewModels.League;
using FootballApp.ViewModels.Fixture;
using Microsoft.EntityFrameworkCore;
using FootballApp.ViewModels.Club;

namespace FootballApp.Services
{
    public class LeagueService : ILeagueService
    {
        private readonly FootballAppDbContext dbContext;

        public LeagueService(FootballAppDbContext context)
        {
            this.dbContext = context;
        }

        public async Task AddLeagueAsync(FormLeagueViewModel model)
        {
            League leagueToAdd = new League()
            {
                Name = model.Name,
                Country = model.Country,
                Logo = model.Logo
            };

            await dbContext.Leagues.AddAsync(leagueToAdd);
            await dbContext.SaveChangesAsync();
        }

        public async Task<bool> DoesLeagueExistsByIdAsync(int leagueId)
        {
            bool res = await dbContext.Leagues.AnyAsync(l => l.Id == leagueId);

            return res;
        }

        public async Task EditLeagueAsync(int leagueId, FormLeagueViewModel model)
        {
            League leagueToEdit = await GetLeagueAsync(leagueId);

            leagueToEdit.Name = model.Name;
            leagueToEdit.Logo = model.Logo;
            leagueToEdit.Country = model.Country;

            await dbContext.SaveChangesAsync();
        }

        public async Task<ClubAddLeagueViewModel> GetAddClubLeagueViewModelAsync(int leagueId)
        {
            ClubAddLeagueViewModel? league = await dbContext.Leagues
                .Where(l => l.Id == leagueId)
                .Select(l => new ClubAddLeagueViewModel()
                {
                    Id = l.Id,
                    Name = l.Name
                })
                .FirstOrDefaultAsync();

            return league!;
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

        public async Task<League> GetLeagueAsync(int leagueId)
        {
            return await dbContext.Leagues.FindAsync(leagueId);
        }

        public async Task<LeaguePageViewModel> GetLeagueByIdAsync(int leagueId)
        {
            var league = await dbContext.Leagues
                .Include(l=>l.Clubs)
                .Include(l=>l.Fixtures)
                .FirstAsync(l=>l.Id == leagueId);

            var model = new LeaguePageViewModel()
            {
                Id = league.Id,
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
                .OrderByDescending(c=>c.Points)
                .ToList(),
                Fixtures = league.Fixtures
                .Select(f=> new AllFixturesViewModel() 
                {
                    Id = f.Id,
                    StartTime = f.StartTime.ToString(),
                    HomeClub = new FixtureClubViewModel()
                    {
                        Id = f.HomeClub.Id,
                        Name = f.HomeClub.Name,
                        Logo = f.HomeClub.Logo
                    },
                    AwayClub = new FixtureClubViewModel()
                    {
                        Id = f.AwayClub.Id,
                        Name = f.AwayClub.Name,
                        Logo = f.AwayClub.Logo
                    },
                }).ToList()
            };
            return model;
        }
        public async Task<List<AddFixtureLeagueViewModel>> GetAddFixtureLeagueViewModelsAsync()
        {
            List<AddFixtureLeagueViewModel> models = await dbContext.Leagues.
                Select(c => new AddFixtureLeagueViewModel()
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToListAsync();

            return models;
        }

    }
}
