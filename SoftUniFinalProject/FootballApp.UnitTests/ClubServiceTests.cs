using FootballApp.Data.Models;
using FootballApp.Data;
using FootballApp.Services.Interfaces;
using FootballApp.Services;
using Microsoft.EntityFrameworkCore;
using FootballApp.ViewModels.Club;

namespace FootballApp.UnitTests
{
    public class ClubServiceTests
    {
        private IEnumerable<Club> _clubs;
        private FootballAppDbContext _context;
        private IClubService clubService;

        [OneTimeSetUp]
        public void TestInitialize()
        {
            this._clubs = new List<Club>()
            {
                new Club()
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
                 },
                new Club()
                {
                    Id = 2,
                    Name = "asdz",
                    Nickname = "asdz",
                    Logo = "asdz",
                    YearOfCreation = 1901,
                    Wins = 1,
                    Draws = 1,
                    Loses = 1,
                    MatchesPlayed = 3,
                    ConcededGoals = 1,
                    ScoredGoals = 1,
                    LeagueId = 1,
                    StadiumId = 2,
                    IsActive = true
                 }

            };
        

            var options = new DbContextOptionsBuilder<FootballAppDbContext>()
                .UseInMemoryDatabase(databaseName: "FAppClubsInMemoryDb") // Use an in-memory DB
                .Options;

                this._context = new FootballAppDbContext(options);
                this._context.Clubs.AddRange(this._clubs); // Add data to the DB
                this._context.SaveChanges();

                clubService = new ClubService(this._context);
        }
        [Test]
        public async Task DoesClubExists_Works()
        {
            int clubId = 1;
            bool res = await clubService.DoesClubExistsByIdAsync(clubId);
            
            Assert.That(res, Is.True);
        }
        [Test]
        public async Task DoesClubExists_Returns_False()
        {
            int clubId = 11;
            bool res = await clubService.DoesClubExistsByIdAsync(clubId);

            Assert.That(res, Is.False);
        }

        [Test]
        public async Task AddClub_Works()
        {
            var model = new FormClubViewModel()
            {
                Name = "newasd",
                Nickname = "newasd",
                Logo = "newasd",
                YearOfCreation = 1900,
                Wins = 1,
                Draws = 1,
                Loses = 1,
                MatchesPlayed = 3,
                ConcededGoals = 1,
                ScoredGoals = 1,
                StadiumId = 1,
                League = new ClubAddLeagueViewModel() { Id = 1, Name = "asd" }

            };

            await clubService.AddClubAsync(model);

            Assert.That(_context.Clubs.Count(), Is.EqualTo(_clubs.Count() + 1));
        }

        [Test]
        public async Task EditClub_Works()
        {
            int clubId = 1;
            var model = new FormClubViewModel()
            {
                Name = "newasd",
                Nickname = "newasd",
                Logo = "newasd",
                YearOfCreation = 1900,
                Wins = 1,
                Draws = 1,
                Loses = 1,
                MatchesPlayed = 3,
                ConcededGoals = 1,
                ScoredGoals = 1,
                StadiumId = 1,

            };

            await clubService.EditClubAsync(clubId, model);
            var club = _context.Clubs.Find(clubId);

            Assert.That(club.Name, Is.EqualTo(model.Name));
            Assert.That(club.Logo, Is.EqualTo(model.Logo));
        }
        [Test]
        public async Task GetClubAsync_Works()
        {
            int clubId = 1;
            var club = await clubService.GetClubAsync(clubId);

            Assert.NotNull(club);
        }

        [Test]
        public async Task GetClubAsync_Returns_Null()
        {
            int clubId = 11;
            var club = await clubService.GetClubAsync(clubId);

            Assert.Null(club);
        }

        [Test]
        public async Task DeleteClubAsync_Works()
        {
            int clubId = 1;
            await clubService.DeleteClubAndReturnLeagueIdAsync(clubId);
            var club = _context.Clubs.Find(clubId);

            Assert.False(club.IsActive);
        }
    }
}
