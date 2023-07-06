using FootballApp.ViewModels.League;

namespace FootballApp.Services.Interfaces
{
    public interface ILeagueService
    {
        public Task<List<AllLeaguesViewModel>> GetAllLeaguesAsync();
    }
}
