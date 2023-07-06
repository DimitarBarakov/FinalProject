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
    }
}
