using FootballApp.Data.Models;
using FootballApp.ViewModels.Player;

namespace FootballApp.Services.Interfaces
{
    public interface IPlayerService
    {
        public Task<PlayerPageViewModel?> GetPlayerPageViewModelByIdAsync(int playerId);
        public Task<Player?> GetPlayerAsync(int playerId);
        public Task<bool> DoesPlayerExistsByIdAsync(int playerId);

        public Task AddPlayerAsync(int id, FormPlayerViewModel model);

        public Task EditPlayerAsync(int id, FormPlayerViewModel model);

        public Task DeletePlayerAsync(int playerId);
    }
}
