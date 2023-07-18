using FootballApp.Services.Interfaces;
using FootballApp.ViewModels.League;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace FootballApp.Controllers
{
    public class LeagueController : Controller
    {
        private readonly ILeagueService leagueService;

        public LeagueController(ILeagueService service)
        {
            this.leagueService = service;
        }

        public async Task<IActionResult> All()
        {
            List<AllLeaguesViewModel> model = await leagueService.GetAllLeaguesAsync();

            return View(model);
        }
        [Route("LeaguePage")]
        public async Task<IActionResult> ShowById(int id)
        {
            bool doesLeagueExists = await leagueService.DoesLeagueExistsByIdAsync(id);


            //TODO: Add error page
            if (!doesLeagueExists)
            {
                return BadRequest();
            }

            var model = await leagueService.GetLeagueByIdAsync(id);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> AddLeague()
        {
            AddLeagueViewModel model = new AddLeagueViewModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddLeague(AddLeagueViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await leagueService.AddLeagueAsync(model);
            return RedirectToAction("All");
        }
    }
}
