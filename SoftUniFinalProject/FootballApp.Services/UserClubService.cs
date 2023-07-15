using FootballApp.Data;
using FootballApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FootballApp.Services
{
    public class UserClubService : IUserClubService
    {
        private readonly FootballAppDbContext dbContext;

        public UserClubService(FootballAppDbContext context)
        {
            dbContext = context;
        }
        public async Task<bool> DoesUserClubExistsAsync(int clubId, string userId)
        {
            bool res = await dbContext.FavoriteClubs.AnyAsync(uc=>uc.UserId==userId && uc.ClubId == clubId);

            return res;
        }
    }
}
