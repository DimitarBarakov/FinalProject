namespace FootballApp.Services.Interfaces
{
    public interface IUserClubService
    {
        Task<bool> DoesUserClubExistsAsync(int clubId, string userId);
    }
}
