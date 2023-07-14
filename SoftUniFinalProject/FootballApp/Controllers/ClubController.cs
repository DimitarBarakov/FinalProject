using FootballApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
            bool doesClubExists = await clubService.DoesHouseExistsByIdAsync(id);
            if (!doesClubExists)
            {
                return BadRequest();
            }
            var model = await clubService.GetClubByIdAsync(id);

            return View(model);
        }
        public async Task<IActionResult> AddToFavorites(int id)
        {
            //TODO: Validate the club and user id, validate if the team is already added
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            await clubService.AddToFavoritesAsync(id, userId);
            return RedirectToAction("ShowFavorites");
        }
    }
}
