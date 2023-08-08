using FootballApp.Data.Models;
using FootballApp.Data;
using FootballApp.Services.Interfaces;
using FootballApp.Services;
using Microsoft.EntityFrameworkCore;
using FootballApp.ViewModels.League;

namespace FootballApp.UnitTests
{
    public class LeagueServiceTests
    {
        private IEnumerable<League> _leagues;
        private FootballAppDbContext _context;
        private ILeagueService leagueService;
        private IClubService clubService;

        [OneTimeSetUp]
        public void TestInitialize()
        {
            this._leagues = new List<League>()
            {
                  new League(){Id = 1, Country = "asd", Name = "asd", Logo = "asd", IsActive = true},
                  new League(){Id = 2, Country = "asdz", Name = "asdz", Logo = "asdz", IsActive = true}
            };

            var options = new DbContextOptionsBuilder<FootballAppDbContext>()
                .UseInMemoryDatabase(databaseName: "FAppPlayersInMemoryDb") // Use an in-memory DB
                .Options;

            this._context = new FootballAppDbContext(options);
            this._context.Leagues.AddRange(this._leagues); // Add data to the DB
            this._context.SaveChanges();

            clubService = new ClubService(this._context);
            leagueService = new LeagueService(this._context, clubService);
        }
        [Test]
        public async Task AddLeagueAsync_Works()
        {
            FormLeagueViewModel model = new FormLeagueViewModel()
            {
                Country = "new",
                Logo = "new",
                Name = "new"
            };

            await this.leagueService.AddLeagueAsync(model);

            int count = _context.Leagues.Count();

            Assert.That(count, Is.EqualTo(_leagues.Count() + 1));
        }

        [Test]
        public async Task DoesLeagueExistsByIdAsync_Works()
        {
            int leagueId = 1;

            bool res = await leagueService.DoesLeagueExistsByIdAsync(leagueId);

            Assert.IsTrue(res);
        }

        [Test]
        public async Task DoesLeagueExistsByIdAsync_Returns_False()
        {
            int leagueId = 11;

            bool res = await leagueService.DoesLeagueExistsByIdAsync(leagueId);

            Assert.IsFalse(res);
        }

        [Test]
        public async Task EditLeagueAsync_Works()
        {
            int leagueId = 1;

            FormLeagueViewModel model = new FormLeagueViewModel()
            {
                Country = "new",
                Logo = "new",
                Name = "new"
            };

            await this.leagueService.EditLeagueAsync(leagueId, model);
            var league = this._context.Leagues.Find(leagueId);


            Assert.That(model.Name, Is.EqualTo(league.Name));
            Assert.That(model.Country, Is.EqualTo(league.Country));
        }

        [Test]
        public async Task GetAllLeaguesAsync_Works()
        {
            var leagues = await leagueService.GetAllLeaguesAsync();


            Assert.That(leagues.Count(), Is.EqualTo(_leagues.Count()));
        }

        [Test]
        public async Task DeleteLeagueAsync_Works()
        {
            var leagueId = 1;
            await leagueService.DeleteLeagueAsync(leagueId);
            var league = this._context.Leagues.Find(leagueId);

            Assert.IsFalse(league.IsActive);
        }

        [Test]
        public async Task GetLeagueAsync_Works()
        {
            var leagueId = 1;

            var league = await leagueService.GetLeagueAsync(leagueId);

            Assert.NotNull(league);
        }

        [Test]
        public async Task GetLeagueAsync_Returns_Null()
        {
            var leagueId = 11;

            var league = await leagueService.GetLeagueAsync(leagueId);

            Assert.Null(league);
        }
    }
}
