using FootballApp.Data;
using FootballApp.Data.Models;
using FootballApp.Services.Interfaces;
using FootballApp.ViewModels.Club;
using FootballApp.ViewModels.Stadium;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace FootballApp.Services
{
    public class StadiumService : IStadiumService
    {
        private readonly FootballAppDbContext dbContext;

        public StadiumService(FootballAppDbContext context)
        {
            dbContext = context;
        }

        public async Task<int> AddStadiumAndReturnIdAsync(StadiumFormViewModel model)
        {
            Stadium stadiumToAdd = new Stadium()
            {
                Location = WebUtility.HtmlEncode(model.Location),
                City = WebUtility.HtmlEncode(model.City),
                Country = WebUtility.HtmlEncode(model.Country),
                Address = WebUtility.HtmlEncode(model.Address),
                Name = WebUtility.HtmlEncode(model.Name),
                Capacity = model.Capacity
            };

            await dbContext.AddAsync(stadiumToAdd);
            await dbContext.SaveChangesAsync();

            return stadiumToAdd.Id;
        }

        public async Task DeleteStadiumAsync(int stadiumId)
        {
            Stadium stadiumToDelete = await GetStadiumByIdAsync(stadiumId);

            stadiumToDelete.IsActive = false;

            await dbContext.SaveChangesAsync();
        }

        public async Task EditStadiumAsync(int stadiumId, StadiumFormViewModel model)
        {
            Stadium stadiumToEdit = await GetStadiumByIdAsync(stadiumId);

            stadiumToEdit.Location = WebUtility.HtmlEncode(model.Location);
            stadiumToEdit.City = WebUtility.HtmlEncode(model.City);
            stadiumToEdit.Country = WebUtility.HtmlEncode(model.Country);
            stadiumToEdit.Address = WebUtility.HtmlEncode(model.Address);
            stadiumToEdit.Name = WebUtility.HtmlEncode(model.Name);
            stadiumToEdit.Capacity = model.Capacity;

            await dbContext.SaveChangesAsync();

        }

        public async Task<Stadium> GetStadiumByIdAsync(int stadiumId)
        {
            Stadium? stadium = await dbContext.Stadiums.Where(s=>s.IsActive)
                .Include(s=>s.Clubs)
                .FirstOrDefaultAsync(s=>s.Id == stadiumId);
            return stadium!;
        }

        public async Task<StadiumPageViewModel?> GetStadiumPageViewModelByIdAsync(int stadiumId)
        {
            Stadium stadium = await GetStadiumByIdAsync(stadiumId);

            if (stadium == null)
            {
                return null;
            }

            StadiumPageViewModel model = new StadiumPageViewModel()
            {
                Id = stadium.Id,
                Location = stadium.Location,
                Name = stadium.Name,
                City = stadium.City,
                Adrress = stadium.Address,
                Country = stadium.Country,
                Capacity = stadium.Capacity
            };

            return model;
        }

        public async Task<List<AddClubStadiumViewModel>> GetStadiumsForAddClubViewModelAsync()
        {
            List<AddClubStadiumViewModel> stadiums = await dbContext.Stadiums
                .Where(s=>s.IsActive)
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
