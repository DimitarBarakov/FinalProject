using FootballApp.Data;
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
    }
}
