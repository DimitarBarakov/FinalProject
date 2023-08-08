using FootballApp.Data.Models;
using FootballApp.Data;
using Microsoft.EntityFrameworkCore;
using FootballApp.Services.Interfaces;
using FootballApp.Services;

namespace FootballApp.UnitTests
{
    [TestFixture]
    public class UserClubServiceTests
    {
        private IEnumerable<UserClub> _usersCLubs;
        private FootballAppDbContext _context;
        private IUserClubService _userClubService;

        [OneTimeSetUp]
        public void TestInitialize()
        {
            Club club = new Club()
            {
                Id = 1,
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
            Club club4 = new Club()
            {
                Id = 4,
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

            this._usersCLubs = new List<UserClub>()
            {
                new UserClub(){ ClubId = 1, UserId = "asd", Club = club },
                new UserClub(){ ClubId = 2, UserId = "asdq"},
                new UserClub(){ ClubId = 3, UserId = "asdqw"},
                new UserClub(){ ClubId = 4, UserId = "asd", Club = club4 }
            };

            var options = new DbContextOptionsBuilder<FootballAppDbContext>()
                .UseInMemoryDatabase(databaseName: "FAppUserClubsInMemoryDb") // Use an in-memory DB
                .Options;

            this._context = new FootballAppDbContext(options);
            this._context.FavoriteClubs.AddRange(this._usersCLubs); // Add data to the DB
            this._context.SaveChanges();

            _userClubService = new UserClubService(_context);
        }

        [Test]
        public async Task DoesUserClubExistsAsync_Works()
        {
            int clubId = 2;
            string userId = "asdq";

            bool res = await _userClubService.DoesUserClubExistsAsync(clubId, userId);

            Assert.True(res);
        }

        [Test]
        public async Task DoesUserClubExistsAsync_Returns_False_With_Wrong_ClubId()
        {
            int clubId = 23;
            string userId = "asdq";

            bool res = await _userClubService.DoesUserClubExistsAsync(clubId, userId);

            Assert.False(res);
        }
        [Test]
        public async Task DoesUserClubExistsAsync_Returns_False_With_Wrong_UserId()
        {
            int clubId = 2;
            string userId = "asdqqweqwe";

            bool res = await _userClubService.DoesUserClubExistsAsync(clubId, userId);

            Assert.False(res);
        }

        //[Test]
        //public async Task GetFavoriteClubsAsync_Works()
        //{
        //    string userId = "asd";

        //    var favoriteClubs = await _userClubService.GetFavoriteClubsAsync(userId);

        //    Assert.That(favoriteClubs.Count(), Is.EqualTo(2));
        //}
    }
}
