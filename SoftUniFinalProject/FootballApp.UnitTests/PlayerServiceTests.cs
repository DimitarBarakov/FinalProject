using FootballApp.Data.Models;
using FootballApp.Data;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using FootballApp.Services.Interfaces;
using FootballApp.Services;
using FootballApp.Data.Migrations;
using FootballApp.ViewModels.Player;

namespace FootballApp.UnitTests
{
    [TestFixture]
    public class PlayerServiceTests
    {
        private IEnumerable<Player> _players;
        private FootballAppDbContext _context;
        private IPlayerService playerService;

        [OneTimeSetUp]
        public void TestInitialize()
        {
            Club club = new Club()
            {
                Name = "asd",
                Nickname = "asd",
                Logo = "asd",
                YearOfCreation = 1900,
                Wins = 1,
                Draws = 1,
                Loses = 1,
                MatchesPlayed = 3,
                ConcededGoals = 1,
                ScoredGoals = 1,
                LeagueId = 1,
                StadiumId = 1,
                IsActive = true
            };

            this._players = new List<Player>()
            {
                new Player() { Id = 1, FirstName = "asdq", LastName = "asdq", Age = 23, Country = "asd", Picture = "asd", Position = "asd", Goals = 1, Assists = 1, MatchesPlayed = 1, Number = 1, IsActive = true, Club = club },
                new Player() { Id = 2, FirstName = "asdqw", LastName = "asdqw", Age = 24, Country = "asd", Picture = "asd", Position = "asd", Goals = 1, Assists = 1, MatchesPlayed = 1, Number = 1, IsActive = true, Club = club },
                new Player() { Id = 3, FirstName = "asdqwe", LastName = "asdqwe", Age = 23, Country = "asd", Picture = "asd", Position = "asd", Goals = 1, Assists = 1, MatchesPlayed = 1, Number = 1, IsActive = true, Club = club },

            };

            var options = new DbContextOptionsBuilder<FootballAppDbContext>()
                .UseInMemoryDatabase(databaseName: "FAppPlayersInMemoryDb") // Use an in-memory DB
                .Options;

            this._context = new FootballAppDbContext(options);
            this._context.Players.AddRange(this._players); // Add data to the DB
            this._context.SaveChanges();

            playerService = new PlayerService(this._context);
        }

        [Test]
        public async Task GetPlayerPageViewModelByIdAsync_Works()
        {

            int playerId = 1;

            var model = await playerService.GetPlayerPageViewModelByIdAsync(playerId);

            Assert.That(model.Id, Is.EqualTo(playerId));
        }

        [Test]
        public async Task DoesPlayerExists_Works()
        {
            Assert.IsTrue(await playerService.DoesPlayerExistsByIdAsync(2));
            Assert.IsFalse(await playerService.DoesPlayerExistsByIdAsync(23));
        }

        [Test]
        public async Task AddPlayer_Works()
        {
            FormPlayerViewModel model = new FormPlayerViewModel()
            {
                FirstName = "newasdq",
                LastName = "newasdq",
                Age = 23,
                Country = "newasd",
                Picture = "newasd",
                Position = "newasd",
                Goals = 2,
                Assists = 2,
                MatchesPlayed = 2,
                Number = 2,
            };
            int countBefore = _context.Players.Count();
            await playerService.AddPlayerAsync(2, model);
            int countAfter = _context.Players.Count();

            Assert.AreEqual(countAfter, countBefore + 1);
        }

        [Test]
        public async Task GetPlayerAsync_Works()
        {
            int playerId = 1;

            var player = await playerService.GetPlayerAsync(playerId);

            Assert.IsNotNull(player);
        }

        [Test]
        public async Task GetPlayerAsync_Returns_Null()
        {
            int playerId = 11;

            var player = await playerService.GetPlayerAsync(playerId);

            Assert.IsNull(player);
        }

        [Test]
        public async Task EditPlayer_Works()
        {
            int playerId = 2;

            FormPlayerViewModel model = new FormPlayerViewModel()
            {
                FirstName = "newasdq",
                LastName = "newasdq",
                Age = 23,
                Country = "newasd",
                Picture = "newasd",
                Position = "newasd",
                Goals = 2,
                Assists = 2,
                MatchesPlayed = 2,
                Number = 2,
            };

            await playerService.EditPlayerAsync(playerId, model);
            var player = _context.Players.Find(playerId);

            Assert.That(player.FirstName, Is.EqualTo(model.FirstName));
            Assert.That(player.LastName, Is.EqualTo(model.LastName));
        }

        [Test]
        public async Task DeletePlayer_Works()
        {
            int playerId = 2;

            await playerService.DeletePlayerAsync(playerId);
            var player = _context.Players.Find(playerId);

            Assert.IsFalse(player.IsActive);
        }
    }
}
