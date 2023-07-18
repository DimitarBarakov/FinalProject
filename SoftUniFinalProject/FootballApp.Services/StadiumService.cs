using FootballApp.Data;
using FootballApp.Services.Interfaces;
using FootballApp.ViewModels.Club;
using Microsoft.EntityFrameworkCore;

namespace FootballApp.Services
{
    public class StadiumService : IStadiumService
    {
        private readonly FootballAppDbContext dbContext;

        public StadiumService(FootballAppDbContext context)
        {
            dbContext = context;
        }
        public async Task<List<AddClubStadiumViewModel>> GetStadiumsForAddClubViewModelAsync()
        {
            List<AddClubStadiumViewModel> stadiums = await dbContext.Stadiums
                .Select(s => new AddClubStadiumViewModel()
                {
                    Id = s.Id,
                    Name = s.Name
                })
                .ToListAsync();

            return stadiums;
        }
    }
}
