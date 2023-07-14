using FootballApp.ViewModels.Player;

namespace FootballApp.Services.Interfaces
{
    public interface IPlayerService
    {
        public Task<PlayerPageViewModel?> GetPlayerByIdAsync(int playerId);
        public Task<bool> DoesPlayerExistsByIdAsync(int playerId);
    }
}
