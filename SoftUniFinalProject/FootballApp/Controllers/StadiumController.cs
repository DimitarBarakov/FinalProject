using FootballApp.Services.Interfaces;
using FootballApp.ViewModels.Stadium;
using Microsoft.AspNetCore.Mvc;

namespace FootballApp.Controllers
{
    public class StadiumController : Controller
    {
        private readonly IStadiumService stadiumService;

        public StadiumController(IStadiumService service)
        {
            stadiumService = service;
        }
        public async Task<IActionResult> StadiumById(int id)
        {
            StadiumPageViewModel model = await stadiumService.GetStadiumPageViewModelByIdAsync(id);

            return View(model);
        }
    }
}
