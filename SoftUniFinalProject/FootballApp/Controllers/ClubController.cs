using FootballApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FootballApp.Controllers
{
    public class ClubController : Controller
    {
        private readonly IClubService clubService;

        public ClubController(IClubService service)
        {
            this.clubService = service;
        }
        public async Task<IActionResult> ClubById(int id)
        {
            var model = await clubService.GetClubById(id);

            return View(model);
        }
    }
}
