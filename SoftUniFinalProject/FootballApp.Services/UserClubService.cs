using FootballApp.Data;
using FootballApp.Data.Models;
using FootballApp.Services.Interfaces;
using FootballApp.ViewModels.Club;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

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

        public async Task<List<FavoriteCLubsViewModel>> GetFavoriteClubsAsync(int clubId, string userId)
        {
            List<FavoriteCLubsViewModel> favoriteClubs = await dbContext.FavoriteClubs
                .Where(fc=>fc.UserId == userId)
                .Select(fc=> new FavoriteCLubsViewModel()
                {
                    Id = fc.Club.Id,
                    Name = fc.Club.Name,
                    Logo = fc.Club.Logo,
                    LeagueLogo = fc.Club.League.Logo
                }).ToListAsync();

            return favoriteClubs;
        }

        public async Task RemoveFromFavorites(int clubId, string userId)
        {
            UserClub? clubToRemove = await dbContext.FavoriteClubs.Where(fc=>fc.UserId == userId).FirstOrDefaultAsync();

            dbContext.FavoriteClubs.Remove(clubToRemove!);
            await dbContext.SaveChangesAsync();
        }
    }
}
