using FootballApp.ViewModels.Club;

namespace FootballApp.Services.Interfaces
{
    public interface IClubService
    {
        public Task<ClubPageViewModel?> GetClubById(int clubId);
    }
}
