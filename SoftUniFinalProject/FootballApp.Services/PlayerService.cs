using FootballApp.Data;
using FootballApp.Data.Models;
using FootballApp.Services.Interfaces;
using FootballApp.ViewModels.Player;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace FootballApp.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly FootballAppDbContext dbContext;

        public PlayerService(FootballAppDbContext context)
        {
            dbContext = context;
        }
        public async Task<PlayerPageViewModel?> GetPlayerByIdAsync(int playerId)
        {
            Player? player = await dbContext.Players
                .Include(p => p.Club)
                .FirstOrDefaultAsync(p => p.Id == playerId);

            PlayerPageViewModel? model = new PlayerPageViewModel()
            {
                Id = player.Id,
                Picture = player!.Picture,
                Name = $"{player.FirstName} {player.LastName}",
                MatchesPlayed = player.MatchesPlayed,
                Goals = player.Goals,
                Assists = player.Assists,
                Country = player.Country,
                Position = player.Position,
                Age = player.Age,   
                Club = new PlayerPageClubViewModel()
                {
                    Logo = player.Club.Logo,
                    Name = player.Club.Name
                }
            };

            return model;
        }
        public async Task<bool> DoesPlayerExistsByIdAsync(int playerId)
        {
            bool doesPlayerExists = await dbContext.Players.AnyAsync(p => p.Id == playerId);

            return doesPlayerExists;
        }

        public async Task AddPlayerAsync(int id, FormPlayerViewModel model)
        {
            Player playerToAdd = new Player()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Age = model.Age,
                Number = model.Number,
                Goals = model.Goals,
                Assists = model.Assists,
                ClubId = id,
                MatchesPlayed = model.MatchesPlayed,
                Country = model.Country,
                Position = model.Position,
                Picture = model.Picture
            };

            await dbContext.Players.AddAsync(playerToAdd);
            await dbContext.SaveChangesAsync();
        }

        public async Task<Player?> GetPlayerAsync(int playerId)
        {
            Player? player = await dbContext.Players.FindAsync(playerId);

            return player;
        }

        public async Task EditPlayerAsync(int id, FormPlayerViewModel model)
        {
            Player? edittedPlayer = await GetPlayerAsync(id);
            edittedPlayer.FirstName = model.FirstName;
            edittedPlayer.LastName = model.LastName;
            edittedPlayer.Age = model.Age;
            edittedPlayer.Number = model.Number;
            edittedPlayer.Goals = model.Goals;
            edittedPlayer.Assists = model.Assists;
            edittedPlayer.MatchesPlayed = model.MatchesPlayed;
            edittedPlayer.Country = model.Country;
            edittedPlayer.Position = model.Position;
            edittedPlayer.Picture = model.Picture;

            await dbContext.SaveChangesAsync();
        }
    }
}
