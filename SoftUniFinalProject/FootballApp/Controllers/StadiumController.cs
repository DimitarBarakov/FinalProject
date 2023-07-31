using FootballApp.Data.Models;
using FootballApp.Services.Interfaces;
using FootballApp.ViewModels.Stadium;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static FootballApp.Common.GeneralConstants;
using static FootballApp.Common.NotificationMessagesConstants;

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

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }
        [HttpGet]
        [Authorize(Roles = AdminRoleName)]
        public async Task<IActionResult> EditStadium(int id)
        {
            Stadium stadium = await stadiumService.GetStadiumByIdAsync(id);

            if (stadium == null)
            {
                return NotFound();
            }

            StadiumFormViewModel model = new StadiumFormViewModel();

            model.Location = stadium.Location;
            model.City = stadium.City;
            model.Country = stadium.Country;
            model.Address = stadium.Address;
            model.Name = stadium.Name;

            return View(model);
        }
        //Only for admin
        [HttpPost]
        [Authorize(Roles = AdminRoleName)]
        public async Task<IActionResult> EditStadium(int id, StadiumFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await stadiumService.EditStadiumAsync(id, model);
            return RedirectToAction("StadiumById", "Stadium", new { id });
        }
        //Only for admin
        [HttpGet]
        [Authorize(Roles = AdminRoleName)]
        public IActionResult AddStadium()
        {
            StadiumFormViewModel model = new StadiumFormViewModel();
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = AdminRoleName)]
        public async Task<IActionResult> AddStadium(StadiumFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            int id = await stadiumService.AddStadiumAndReturnIdAsync(model);
            return RedirectToAction("StadiumById", "Stadium", new { id });
        }

        [HttpPost]
        [Authorize(Roles = AdminRoleName)]
        public async Task<IActionResult> DeleteStadium(int id)
        {
            Stadium stadium = await stadiumService.GetStadiumByIdAsync(id);

            if (stadium.Clubs.Any())
            {
                TempData[ErrorMessage] = "A team is playing on that stadium and it can not be deleted!";

                return RedirectToAction("StadiumById", new { id });
            }

            await stadiumService.DeleteStadiumAsync(id);
            return RedirectToAction("Index", "Home");
        }
    }
}
