﻿using FootballApp.Data;
using FootballApp.Data.Models;
using FootballApp.Services.Interfaces;
using FootballApp.ViewModels.Club;
using FootballApp.ViewModels.Fixture;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace FootballApp.Services
{
    public class ClubService : IClubService
    {
        private readonly FootballAppDbContext dbContext;

        public ClubService(FootballAppDbContext context)
        {
            dbContext = context;
        }
        public async Task<ClubPageViewModel?> GetClubPageViewModelByIdAsync(int clubId)
        {
            Club? club = await dbContext.Clubs
                .Where(c=>c.IsActive)
                .Include(c=>c.HomeFixtures)
                .ThenInclude(f=>f.AwayClub)
                .Include(c=>c.AwayFixtures)
                .ThenInclude(f=>f.HomeClub)
                .Include(c=>c.Players)
                .Include(c=>c.Stadium)
                .FirstOrDefaultAsync(c=>c.Id == clubId);

            List<AllFixturesViewModel> awayFixtures = club!.AwayFixtures
                .Where(af=>af.IsActive)
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
                Stadium = new ClubPageStadiumViewModel
                {
                    Id = club.Stadium.Id,
                    Name = club.Stadium.Name
                },
                Players = club.Players
                .Where(p=>p.IsActive)
                .Select(p => new ClubPagePlayerViewModel()
                {
                    Id = p.Id,
                    Name = $"{p.FirstName} {p.LastName}",
                    Position = p.Position,
                    Number = p.Number
                })
                .ToList(),
                Fixtures = club.HomeFixtures
                .Where(hf => hf.IsActive)
                .Select(f => new AllFixturesViewModel()
                {
                    Id = f.Id,
                    StartTime = f.StartTime.ToString("dd/MM/yyyy hh:mm tt"),
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
                .OrderBy(f =>DateTime.Parse(f.StartTime))
                .ToList()
            };

            return model;
        }

        

        public async Task<bool> DoesClubExistsByIdAsync(int clubId)
        {
            bool result = await dbContext.Clubs.AnyAsync(c => c.Id == clubId && c.IsActive);

            return result;
        }

        public async Task AddClubAsync(FormClubViewModel model)
        {
            Club clubToAdd = new Club()
            {
                Name = WebUtility.HtmlEncode(model.Name),
                Nickname = WebUtility.HtmlEncode(model.Nickname),
                Logo = WebUtility.HtmlEncode(model.Logo),
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

        public async Task EditClubAsync(int id, FormClubViewModel model)
        {
            Club clubToEdit = await GetClubAsync(id);

            clubToEdit.Name = WebUtility.HtmlEncode(model.Name);
            clubToEdit.Nickname = WebUtility.HtmlEncode(model.Nickname);
            clubToEdit.Logo = WebUtility.HtmlEncode(model.Logo);
            clubToEdit.YearOfCreation = model.YearOfCreation;
            clubToEdit.Wins = model.Wins;
            clubToEdit.Loses = model.Loses;
            clubToEdit.Draws = model.Draws;
            clubToEdit.MatchesPlayed = model.MatchesPlayed;
            clubToEdit.ScoredGoals = model.ScoredGoals;
            clubToEdit.ConcededGoals = model.ConcededGoals;
            clubToEdit.StadiumId = model.StadiumId;

            await dbContext.SaveChangesAsync();
        }

        public async Task<Club> GetClubAsync(int clubId)
        {
            Club? club = await dbContext.Clubs
                .Where(c=>c.IsActive)
                .Include(c=>c.Players)
                .Include(c=>c.AwayFixtures)
                .Include(c=>c.HomeFixtures)
                .FirstOrDefaultAsync(c=>c.Id == clubId);

            return club!;
        }

        public async Task<List<AddFixtureClubViewModel>> GetAddFixtureClubViewModelsAsync()
        {
            List<AddFixtureClubViewModel> models = await dbContext.Clubs
                .Where(c=>c.IsActive)
                .Select(c => new AddFixtureClubViewModel()
                {
                    Id = c.Id,
                    Name = c.Name,
                    LeagueId = c.LeagueId
                })
                .ToListAsync();

            return models;
        }

        public async Task<int> DeleteClubAndReturnLeagueIdAsync(int clubId)
        {
            Club clubToDelete = await GetClubAsync(clubId);

            clubToDelete.IsActive = false;
            clubToDelete.Players.Select(p => p.IsActive = false).ToList();
            clubToDelete.AwayFixtures.Select(af => af.IsActive = false).ToList();
            clubToDelete.HomeFixtures.Select(hf => hf.IsActive = false).ToList();

            await dbContext.SaveChangesAsync();

            return clubToDelete.LeagueId;
        }
    }
}
