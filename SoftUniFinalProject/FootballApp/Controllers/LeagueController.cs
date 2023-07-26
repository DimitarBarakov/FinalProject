using FootballApp.Data.Models;
using FootballApp.Services.Interfaces;
using FootballApp.ViewModels.League;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static FootballApp.Common.GeneralConstants;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Data;

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

            if (!doesLeagueExists)
            {
                return BadRequest();
            }

            var model = await leagueService.GetLeagueByIdAsync(id);
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = AdminRoleName)]
        public IActionResult AddLeague()
        {
            FormLeagueViewModel model = new FormLeagueViewModel();

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = AdminRoleName)]
        public async Task<IActionResult> AddLeague(FormLeagueViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await leagueService.AddLeagueAsync(model);
            return RedirectToAction("All");
        }

        [HttpGet]
        [Authorize(Roles = AdminRoleName)]
        public async Task<IActionResult> EditLeague(int id)
        {
            League league = await leagueService.GetLeagueAsync(id);

            FormLeagueViewModel model = new FormLeagueViewModel();
            model.Name = league.Name;
            model.Country = league.Country;
            model.Logo = league.Logo;

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = AdminRoleName)]
        public async Task<IActionResult> EditLeague(int id, FormLeagueViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await leagueService.EditLeagueAsync(id, model);
            return RedirectToAction("ShowById", "League", new {id});
        }
    }
}
