using FootballApp.Data;
using FootballApp.Data.Models;
using FootballApp.Services.Interfaces;
using FootballApp.ViewModels.Club;
using FootballApp.ViewModels.Stadium;
using Microsoft.EntityFrameworkCore;

namespace FootballApp.Services
{
    public class StadiumService : IStadiumService
    {
        private readonly FootballAppDbContext dbContext;

        public StadiumService(FootballAppDbContext context)
        {
            dbContext = context;
        }

        public async Task EditStadiumAsync(int stadiumId, StadiumFormViewModel model)
        {
            Stadium stadiumToEdit = await GetStadiumByIdAsync(stadiumId);

            stadiumToEdit.Location = model.Location;
            stadiumToEdit.City = model.City;
            stadiumToEdit.Country = model.Country;
            stadiumToEdit.Address = model.Address;
            stadiumToEdit.Name = model.Name;

            await dbContext.SaveChangesAsync();

        }

        public async Task<Stadium> GetStadiumByIdAsync(int stadiumId)
        {
            return await dbContext.Stadiums.FindAsync(stadiumId);
        }

        public async Task<StadiumPageViewModel> GetStadiumPageViewModelByIdAsync(int stadiumId)
        {
            Stadium stadium = await GetStadiumByIdAsync(stadiumId);

            StadiumPageViewModel model = new StadiumPageViewModel()
            {
                Id = stadium.Id,
                Location = stadium.Location,
                Name = stadium.Name,
                City = stadium.City,
                Adrress = stadium.Address,
                Country = stadium.Country
            };

            return model;
        }

        public async Task<List<AddClubStadiumViewModel>> GetStadiumsForAddClubViewModelAsync()
        {
            List<AddClubStadiumViewModel> stadiums = await dbContext.Stadiums
                .Select(s => new AddClubStadiumViewModel()
                {
                    Id = s.Id,
                    Name = s.Name
                })
                .ToListAsync();

            return stadiums;
        }
    }
}
