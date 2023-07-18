using FootballApp.ViewModels.Club;
using FootballApp.ViewModels.League;

namespace FootballApp.Services.Interfaces
{
    public interface ILeagueService
    {
        public Task<List<AllLeaguesViewModel>> GetAllLeaguesAsync();

        public Task<LeaguePageViewModel> GetLeagueByIdAsync(int leagueId);

        public Task<bool> DoesLeagueExistsByIdAsync(int leagueId);

        public Task<ClubAddLeagueViewModel> GetAddClubLeagueViewModelAsync(int leagueId);

        public Task AddLeagueAsync(AddLeagueViewModel model);
    }
}
