using FootballApp.ViewModels.Club;

namespace FootballApp.Services.Interfaces
{
    public interface IStadiumService
    {
        public Task<List<AddClubStadiumViewModel>> GetStadiumsForAddClubViewModelAsync();
    }
}
