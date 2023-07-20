using FootballApp.Data.Models;
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

        [HttpGet]
        public async Task<IActionResult> EditStadium(int id)
        {
            Stadium stadium = await stadiumService.GetStadiumByIdAsync(id);

            StadiumFormViewModel model = new StadiumFormViewModel();

            model.Location = stadium.Location;
            model.City = stadium.City;
            model.Country = stadium.Country;
            model.Address = stadium.Address;
            model.Name = stadium.Name;

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditStadium(int id, StadiumFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await stadiumService.EditStadiumAsync(id, model);
            return RedirectToAction("StadiumById", "Stadium", new { id });
        }
    }
}
