using FootballApp.Data;
using FootballApp.Data.Models;
using FootballApp.Services.Interfaces;
using FootballApp.ViewModels.Club;
using FootballApp.ViewModels.Fixture;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace FootballApp.Services
{
    public class ClubService : IClubService
    {
        private readonly FootballAppDbContext dbContext;

        public ClubService(FootballAppDbContext context)
        {
            dbContext = context;
        }
        public async Task<ClubPageViewModel?> GetClubByIdAsync(int clubId)
        {
            Club? club = await dbContext.Clubs
                .Include(c=>c.HomeFixtures)
                .ThenInclude(f=>f.AwayClub)
                .Include(c=>c.AwayFixtures)
                .ThenInclude(f=>f.HomeClub)
                .Include(c=>c.Players)
                .Include(c=>c.Stadium)
                .FirstOrDefaultAsync(c=>c.Id == clubId);

            List<AllFixturesViewModel> awayFixtures = club!.AwayFixtures
                .Select(f => new AllFixturesViewModel()
                {
                    Id = f.Id,
                    StartTime = f.StartTime.ToString(),
                    HomeClub = new FixtureClubViewModel()
                    {
                        Id = f.HomeClub.Id,
                        Name = f.HomeClub.Name,
                        Logo = f.HomeClub.Logo
                    },
                    AwayClub = new FixtureClubViewModel()
                    {
                        Id = f.AwayClub.Id,
                        Name = f.AwayClub.Name,
                        Logo = f.AwayClub.Logo
                    },
                }).ToList();


            ClubPageViewModel model = new ClubPageViewModel()
            {
                Id = club.Id,
                Logo = club.Logo,
                Name = club.Name,
                Nickname = club.Nickname,
                Stadium = club.Stadium,
                Players = club.Players
                .Select(p => new ClubPagePlayerViewModel()
                {
                    Id = p.Id,
                    Name = $"{p.FirstName} {p.LastName}",
                    Position = p.Position,
                    Number = p.Number
                })
                .ToList(),
                Fixtures = club.HomeFixtures
                .Select(f => new AllFixturesViewModel()
                {
                    Id = f.Id,
                    StartTime = f.StartTime.ToString(),
                    HomeClub = new FixtureClubViewModel()
                    {
                        Id = f.HomeClub.Id,
                        Name = f.HomeClub.Name,
                        Logo = f.HomeClub.Logo
                    },
                    AwayClub = new FixtureClubViewModel()
                    {
                        Id = f.AwayClub.Id,
                        Name = f.AwayClub.Name,
                        Logo = f.AwayClub.Logo
                    },
                }).Concat(awayFixtures)
                .ToList()
            };

            return model;
        }

        public async Task AddToFavoritesAsync(int clubId, string userId)
        {
            Club? club = await dbContext.Clubs.FindAsync(clubId);
            UserClub userClub = new UserClub()
            {
                ClubId = club!.Id,
                UserId = userId
            };

            await dbContext.FavoriteClubs.AddAsync(userClub);
            dbContext.SaveChanges();
        }

        public async Task<bool> DoesClubExistsByIdAsync(int clubId)
        {
            bool result = await dbContext.Clubs.AnyAsync(c => c.Id == clubId);

            return result;
        }

        public async Task AddClubAsync(AddClubViewModel model)
        {
            Club clubToAdd = new Club()
            {
                Name = model.Name,
                Nickname = model.Nickname,
                Logo = model.Logo,
                YearOfCreation = model.YearOfCreation,
                Wins = model.Wins,
                Loses = model.Loses,
                Draws = model.Draws,
                MatchesPlayed = model.MatchesPlayed,
                ScoredGoals = model.ScoredGoals,
                ConcededGoals = model.ConcededGoals,
                LeagueId = model.League.Id,
                StadiumId = model.StadiumId
            };

            await dbContext.Clubs.AddAsync(clubToAdd);
            await dbContext.SaveChangesAsync();
        }
    }
}
