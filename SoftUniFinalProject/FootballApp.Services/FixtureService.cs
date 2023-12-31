﻿using FootballApp.Data;
using FootballApp.Data.Models;
using FootballApp.Services.Interfaces;
using FootballApp.ViewModels.Fixture;
using Microsoft.EntityFrameworkCore;

namespace FootballApp.Services
{
    public class FixtureService : IFixtureService
    {
        private readonly FootballAppDbContext dbContext;

        public FixtureService(FootballAppDbContext context)
        {
            dbContext = context;
        }

        public async Task AddFixtureAsync(FixtureAddViewModel model)
        {
            Fixture fixtureToAdd = new Fixture()
            {
                HomeClubId = model.HomeClubId,
                AwayClubId = model.AwayClubId,
                StartTime = model.StartTime,
                LeagueId = model.LeagueId
            };

            await dbContext.Fixtures.AddAsync(fixtureToAdd);
            await dbContext.SaveChangesAsync();
        }

        public async Task<List<AllFixturesViewModel>> GetAllFixturesAsync()
        {
            var fixtures = await dbContext.Fixtures
                .Where(f => f.IsActive)
                .Include(f => f.HomeClub)
                .Include(f => f.AwayClub)
                .OrderBy(f=>f.StartTime)
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
                })
                .ToListAsync();
            return fixtures;
        }

        public async Task<Fixture> GetFixtureAsync(int fixtureId)
        {
            Fixture? fixture = await dbContext.Fixtures
                .Where(f=>f.IsActive)
                .Include(f => f.HomeClub)
                .ThenInclude(c=>c.Stadium)
                .Include(f => f.AwayClub)
                .FirstOrDefaultAsync(f => f.Id == fixtureId);
            return fixture!;
        }

        public async Task<AllFixturesViewModel> GetFixtureViewModelByIdAsync(int fixtureId)
        {
            Fixture fixture = await GetFixtureAsync(fixtureId);

            AllFixturesViewModel viewModel = new AllFixturesViewModel()
            {
                Id = fixture.Id,
                StartTime = fixture.StartTime.ToString("dd/MM/yyyy hh:mm tt"),
                AwayClub = new FixtureClubViewModel
                {
                    Name = fixture.AwayClub.Name,
                    Id = fixture.AwayClub.Id,
                    Logo = fixture.AwayClub.Logo
                },
                HomeClub = new FixtureClubViewModel
                {
                    Name = fixture.HomeClub.Name,
                    Id = fixture.HomeClub.Id,
                    Logo = fixture.HomeClub.Logo
                },
                Stadium = new FixtureStadiumViewModel()
                {
                    Id = fixture.Stadium.Id,
                    Name = fixture.Stadium.Name
                }
            };

            return viewModel;
        }

        public async Task DeleteFixtureAsync(int fixtureId)
        {
            Fixture fixtureToDelete = await GetFixtureAsync(fixtureId);

            fixtureToDelete.IsActive = false;

            await dbContext.SaveChangesAsync();
        }

        public async Task EditFixtureAsync(int fixtureId, EditFixtureViewModel model)
        {
            var fixtureToEdit = await GetFixtureAsync(fixtureId);

            fixtureToEdit.StartTime = model.StartTime;

            await dbContext.SaveChangesAsync();
        }
    }
}
