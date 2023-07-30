using FootballApp.Data;
using FootballApp.Data.Models;
using FootballApp.Services;
using FootballApp.Services.Interfaces;
using FootballApp.ViewModels.Stadium;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballApp.UnitTests
{
    [TestFixture]
    public class StadiumServiceTests
    {
        private IEnumerable<Stadium> _stadiums;
        private FootballAppDbContext _context;

        [OneTimeSetUp]
        public void TestInitialize()
        {
            this._stadiums = new List<Stadium>()
            {
                new Stadium(){Id = 1, Address = "asd",City = "asd",Location = "asd",Capacity = 10000, Country = "asd", Name = "asd"},
                new Stadium(){Id = 2, Address = "asdz",City = "asdz",Location = "asdz",Capacity = 10001, Country = "asdz", Name = "asdz"},
                new Stadium(){Id = 3, Address = "asdzx",City = "asdzx",Location = "asdzx",Capacity = 10011, Country = "asdzx", Name = "asdzx"},
            };

            var options = new DbContextOptionsBuilder<FootballAppDbContext>()
                .UseInMemoryDatabase(databaseName: "FAppStadiumsInMemoryDb") // Use an in-memory DB
                .Options;

            this._context = new FootballAppDbContext(options);
            this._context.Stadiums.AddRange(this._stadiums); // Add data to the DB
            this._context.SaveChanges();
        }
        [Test]
        public async Task GetStadiumById_Returns_Null()
        {
            IStadiumService stadiumService = new StadiumService(this._context);

            Stadium nullStadium = await stadiumService.GetStadiumByIdAsync(23);

            Assert.Null(nullStadium);
        }

        [Test]
        public async Task GetStadiumById_Returns_Correct_Stadium()
        {
            IStadiumService stadiumService = new StadiumService(this._context);

            Stadium searchedStadium = await stadiumService.GetStadiumByIdAsync(2);

            Stadium correctStadium = _context.Stadiums.Find(2);

            Assert.True(searchedStadium.Name == correctStadium.Name);
        }

        [Test]
        public async Task EditStadium_Works()
        {
            int stadiumId = 2;

            IStadiumService stadiumService = new StadiumService(this._context);


            StadiumFormViewModel model = new StadiumFormViewModel()
            {
                Address = "editasdz",
                City = "editasdz",
                Location = "editasdz",
                Capacity = 10001,
                Country = "editasdz",
                Name = "editasdz"
            };

            await stadiumService.EditStadiumAsync(stadiumId, model);

            Stadium stadiumToEdit = await stadiumService.GetStadiumByIdAsync(stadiumId);

            Assert.True(stadiumToEdit.Location == model.Location);
            Assert.True(stadiumToEdit.Capacity == model.Capacity);
            Assert.True(stadiumToEdit.Name == model.Name);
        }

        [Test]
        public async Task EditStadium_ThrowsException()
        {
            int stadiumId = 23;

            IStadiumService stadiumService = new StadiumService(this._context);

            StadiumFormViewModel model = new StadiumFormViewModel()
            {
                Address = "editasdz",
                City = "editasdz",
                Location = "editasdz",
                Capacity = 10001,
                Country = "editasdz",
                Name = "editasdz"
            };

            //Assert.True(stadiumToEdit.Location == model.Location);

            Assert.Throws<NullReferenceException>(() => stadiumService.EditStadiumAsync(stadiumId, model));

            //Assert.That(()=>stadiumService.EditStadiumAsync(stadiumId, model), Throws.NullReferenceException);
        }
    }
}
