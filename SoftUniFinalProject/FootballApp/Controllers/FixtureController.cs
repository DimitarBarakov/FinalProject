using FootballApp.Data.Models;
using FootballApp.Services.Interfaces;
using FootballApp.ViewModels.Fixture;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static FootballApp.Common.GeneralConstants;
using System.Data;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = AdminRoleName)]
        public async Task<IActionResult> AddFixture()
        { 
            FixtureAddViewModel model = new FixtureAddViewModel();
            model.Leagues = await leagueService.GetAddFixtureLeagueViewModelsAsync();
            model.Clubs = await clubService.GetAddFixtureClubViewModelsAsync();

            return View(model);
        }
        [HttpPost]
        [Authorize(Roles = AdminRoleName)]
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

        [HttpPost]
        [Authorize(Roles = AdminRoleName)]
        public async Task<IActionResult> DeleteFixture(int id)
        {
            await fixtureService.DeleteFixtureAsync(id);

            return RedirectToAction("All");
        }

        [HttpGet]
        [Authorize(Roles = AdminRoleName)]
        public async Task<IActionResult> EditFixture(int id)
        {
            var fixture = await fixtureService.GetFixtureAsync(id);
            var model = new EditFixtureViewModel();
            model.StartTime = fixture.StartTime;

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = AdminRoleName)]
        public async Task<IActionResult> EditFixture(int id, EditFixtureViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await fixtureService.EditFixtureAsync(id, model);
            return RedirectToAction("FixtureById", "Fixture", new { id });

        }
    }
}
