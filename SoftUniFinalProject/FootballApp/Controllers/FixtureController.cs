using FootballApp.Services.Interfaces;
using FootballApp.ViewModels.Fixture;
using Humanizer.Localisation.DateToOrdinalWords;
using Microsoft.AspNetCore.Mvc;

namespace FootballApp.Controllers
{
    public class FixtureController : Controller
    {
        private readonly IFixtureService fixtureService;

        public FixtureController(IFixtureService service)
        {
            fixtureService = service;
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
    }
}
