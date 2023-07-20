using FootballApp.Data.Models;
using FootballApp.ViewModels.Club;
using FootballApp.ViewModels.Fixture;

namespace FootballApp.Services.Interfaces
{
    public interface IClubService
    {
        public Task<ClubPageViewModel?> GetClubByIdAsync(int clubId);

        public Task AddToFavoritesAsync(int clubId, string userId);

        public Task<bool> DoesClubExistsByIdAsync(int houseId);

        public Task AddClubAsync(FormClubViewModel model);

        public Task EditClubAsync(int id, FormClubViewModel model);

        public Task<Club> GetClubAsync(int clubId);

        public Task<List<AddFixtureClubViewModel>> GetAddFixtureClubViewModelsAsync();
    }
}
