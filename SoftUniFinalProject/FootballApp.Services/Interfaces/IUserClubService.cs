using FootballApp.ViewModels.Club;

namespace FootballApp.Services.Interfaces
{
    public interface IUserClubService
    {
        Task<bool> DoesUserClubExistsAsync(int clubId, string userId);
        Task<List<FavoriteCLubsViewModel>> GetFavoriteClubsAsync(int clubId, string userId);
    }
}
