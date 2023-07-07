using FootballApp.Data;
using FootballApp.Data.Models;
using FootballApp.Services.Interfaces;
using FootballApp.ViewModels.Club;
using Microsoft.EntityFrameworkCore;

namespace FootballApp.Services
{
    public class ClubService : IClubService
    {
        private readonly FootballAppDbContext dbContext;

        public ClubService(FootballAppDbContext context)
        {
            dbContext = context;
        }
        public async Task<ClubPageViewModel?> GetClubById(int clubId)
        {
            Club? club = await dbContext.Clubs
                .Include(c=>c.Players)
                .Include(c=>c.Stadium)
                .FirstOrDefaultAsync(c=>c.Id == clubId);
            ClubPageViewModel model = new ClubPageViewModel()
            {
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
                .ToList()
            };

            return model;
        }
    }
}
