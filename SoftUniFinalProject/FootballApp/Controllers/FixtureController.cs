﻿using FootballApp.Data.Models;
using FootballApp.Services.Interfaces;
using FootballApp.ViewModels.Fixture;
using Microsoft.AspNetCore.Mvc;

namespace FootballApp.Controllers
{
    public class FixtureController : Controller
    {
        private readonly IFixtureService fixtureService;
        private readonly ILeagueService leagueService;
        private readonly IClubService clubService;

        public FixtureController(IFixtureService service, ILeagueService lService, IClubService cService)
        {
            fixtureService = service;
            clubService = cService;
            leagueService = lService;
        }
        public async Task<IActionResult> All()
        {
            var model = await fixtureService.GetAllFixturesAsync();

            return View(model);
        }

        public async Task<IActionResult> FixtureById(int id)
        {
            AllFixturesViewModel model = await fixtureService.GetFixtureViewModelByIdAsync(id);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> AddFixture()
        { 
            FixtureAddViewModel model = new FixtureAddViewModel();
            model.Leagues = await leagueService.GetAddFixtureLeagueViewModelsAsync();
            model.Clubs = await clubService.GetAddFixtureClubViewModelsAsync();

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> AddFixture(FixtureAddViewModel model)
        {
            Club homeClub = await clubService.GetClubAsync(model.HomeClubId);
            Club awayClub = await clubService.GetClubAsync(model.AwayClubId);

            if (homeClub.LeagueId != model.LeagueId)
            {
                ModelState.AddModelError(nameof(model.HomeClubId), "The team is not from the selected league");
            }
            else if (awayClub.LeagueId != model.LeagueId)
            {
                ModelState.AddModelError(nameof(model.AwayClubId), "The team is not from the selected league");
            }

            if (model.AwayClubId == model.HomeClubId)
            {
                ModelState.AddModelError(nameof(model.AwayClubId), "A team cannot play against itsself");
            }
            if (!ModelState.IsValid)
            {
                model.Leagues = await leagueService.GetAddFixtureLeagueViewModelsAsync();
                model.Clubs = await clubService.GetAddFixtureClubViewModelsAsync();
                return View(model);
            }

            await fixtureService.AddFixtureAsync(model);
            return RedirectToAction("All");
        }
    }
}
