using FootballApp.Data;
using FootballApp.Services.Interfaces;
using FootballApp.ViewModels.Fixture;
using Microsoft.EntityFrameworkCore;

namespace FootballApp.Services
{
    public class FixtureService : IFixtureService
    {
        private readonly FootballAppDbContext dbContext;

        public FixtureService(FootballAppDbContext context)
        {
            dbContext = context;
        }
        public async Task<List<AllFixturesViewModel>> GetAllFixturesAsync()
        {
            var fixtures = await dbContext.Fixtures
                .Include(f => f.HomeClub)
                .Include(f => f.AwayClub)
                .Select(f => new AllFixturesViewModel()
                {
                    Id = f.Id,
                    StartTime = f.StartTime.ToString("dd/MM/yyyy hh:mm tt"),
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
                    Stadium = new FixtureStadiumViewModel()
                    {
                        Id = f.HomeClub.Stadium.Id,
                        Name = f.HomeClub.Stadium.Name,
                    }
                })
                .ToListAsync();
            return fixtures;
        }
    }
}
