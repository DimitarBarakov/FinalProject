using FootballApp.Data.Models;
using FootballApp.ViewModels.Fixture;

namespace FootballApp.Services.Interfaces
{
    public interface IFixtureService
    {
        public Task<List<AllFixturesViewModel>> GetAllFixturesAsync();

        public Task<AllFixturesViewModel> GetFixtureViewModelByIdAsync(int id);

        public Task<Fixture> GetFixtureAsync(int id);
    }
}
