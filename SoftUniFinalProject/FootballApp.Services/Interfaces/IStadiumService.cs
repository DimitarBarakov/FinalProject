using FootballApp.Data.Models;
using FootballApp.ViewModels.Club;
using FootballApp.ViewModels.Stadium;

namespace FootballApp.Services.Interfaces
{
    public interface IStadiumService
    {
        public Task<List<AddClubStadiumViewModel>> GetStadiumsForAddClubViewModelAsync();

        public Task<StadiumPageViewModel> GetStadiumPageViewModelByIdAsync(int stadiumId);

        public Task<Stadium> GetStadiumByIdAsync(int stadiumId);

        public Task EditStadiumAsync(int stadiumId, StadiumFormViewModel model);

        public Task<int> AddStadiumAndReturnIdAsync(StadiumFormViewModel model);

        public Task DeleteStadiumAsync(int stadiumId);
    }
}
