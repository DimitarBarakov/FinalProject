using FootballApp.Data.Models;
using FootballApp.ViewModels.Club;
using Microsoft.EntityFrameworkCore;

namespace FootballApp.Services.Interfaces
{
    public interface IUserClubService
    {
        Task<bool> DoesUserClubExistsAsync(int clubId, string userId);
        Task<List<FavoriteCLubsViewModel>> GetFavoriteClubsAsync(int clubId, string userId);

        Task RemoveFromFavorites(int clubId, string userId);
        public Task AddToFavoritesAsync(int clubId, string userId);
    }
}
