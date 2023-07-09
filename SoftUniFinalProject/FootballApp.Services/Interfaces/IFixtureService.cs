using FootballApp.ViewModels.Fixture;

namespace FootballApp.Services.Interfaces
{
    public interface IFixtureService
    {
        public Task<List<AllFixturesViewModel>> GetAllFixturesAsync();
    }
}
