using FootballApp.Data;
using FootballApp.Data.Models;
using FootballApp.Services.Interfaces;
using FootballApp.ViewModels.League;
using FootballApp.ViewModels.Fixture;
using Microsoft.EntityFrameworkCore;
using FootballApp.ViewModels.Club;
using System.Net;

namespace FootballApp.Services
{
    public class LeagueService : ILeagueService
    {
        private readonly FootballAppDbContext dbContext;
        private readonly IClubService clubService;

        public LeagueService(FootballAppDbContext context, IClubService cService)
        {
            this.dbContext = context;
            clubService = cService;
        }

        public async Task AddLeagueAsync(FormLeagueViewModel model)
        {
            League leagueToAdd = new League()
            {
                Name = WebUtility.HtmlEncode(model.Name),
                Country = WebUtility.HtmlEncode(model.Country),
                Logo = WebUtility.HtmlEncode(model.Logo)
        };

            await dbContext.Leagues.AddAsync(leagueToAdd);
            await dbContext.SaveChangesAsync();
        }

        public async Task<bool> DoesLeagueExistsByIdAsync(int leagueId)
        {
            bool res = await dbContext.Leagues.AnyAsync(l => l.Id == leagueId && l.IsActive);

            return res;
        }

        public async Task EditLeagueAsync(int leagueId, FormLeagueViewModel model)
        {
            League leagueToEdit = await GetLeagueAsync(leagueId);

            leagueToEdit.Name = WebUtility.HtmlEncode(model.Name);
            leagueToEdit.Logo = WebUtility.HtmlEncode(model.Logo);
            leagueToEdit.Country = WebUtility.HtmlEncode(model.Country);

            await dbContext.SaveChangesAsync();
        }

        public async Task<ClubAddLeagueViewModel> GetAddClubLeagueViewModelAsync(int leagueId)
        {
            ClubAddLeagueViewModel? league = await dbContext.Leagues
                .Where(l => l.Id == leagueId && l.IsActive)
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
                .Where(l=>l.IsActive)
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
            return await dbContext.Leagues
                .Where(l => l.IsActive)
                .Include(l=>l.Clubs)
                .FirstOrDefaultAsync(l => l.Id == leagueId);
        }

        public async Task<LeaguePageViewModel> GetLeaguePageViewModelByIdAsync(int leagueId)
        {
            var league = await dbContext.Leagues
                .Where(l=>l.IsActive)
                .Include(l => l.Clubs)
                .Include(l => l.Fixtures)
                .FirstOrDefaultAsync(l => l.Id == leagueId);

            var model = new LeaguePageViewModel()
            {
                Id = league.Id,
                Name = league.Name,
                Country = league.Country,
                Logo = league.Logo,
                Clubs = league.Clubs
                .Where(c=>c.IsActive)
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
                .OrderByDescending(c => c.Points)
                .ThenByDescending(c=>c.GoalDifferrence)
                .ToList(),
                Fixtures = league.Fixtures
                .Where(f=>f.IsActive)
                .Select(f => new AllFixturesViewModel()
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
            List<AddFixtureLeagueViewModel> models = await dbContext.Leagues
                .Where(l=>l.IsActive)
                .Select(c => new AddFixtureLeagueViewModel()
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToListAsync();

            return models;
        }

        public async Task DeleteLeagueAsync(int leagueId)
        {
            League leagueToDelete = await GetLeagueAsync(leagueId);

            leagueToDelete.IsActive = false;
            foreach (var club in leagueToDelete.Clubs)
            {
                await clubService.DeleteClubAndReturnLeagueIdAsync(club.Id);   
            }

            await dbContext.SaveChangesAsync();
        }
    }
}
