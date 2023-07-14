using FootballApp.ViewModels.Club;

namespace FootballApp.Services.Interfaces
{
    public interface IClubService
    {
        public Task<ClubPageViewModel?> GetClubByIdAsync(int clubId);

        public Task AddToFavoritesAsync(int clubId, string userId);

        public Task<bool> DoesHouseExistsByIdAsync(int houseId);
    }
}
