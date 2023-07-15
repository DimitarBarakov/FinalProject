using FootballApp.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FootballApp.Controllers
{
    public class ClubController : Controller
    {
        private readonly IClubService clubService;
        private readonly IUserClubService userClubService;

        public ClubController(IClubService service, IUserClubService ucservice)
        {
            this.clubService = service;
            userClubService = ucservice;
        }
        public async Task<IActionResult> ClubById(int id)
        {
            bool doesClubExists = await clubService.DoesClubExistsByIdAsync(id);
            if (!doesClubExists)
            {
                return NotFound();
            }
            var model = await clubService.GetClubByIdAsync(id);

            return View(model);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddToFavorites(int id)
        {
            //TODO: Validate the club and user id, validate if the team is already added
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var club = clubService.GetClubByIdAsync(id);
            if (club == null)
            {
                return BadRequest("Club with this Id does not exists!");
            }
            if (await userClubService.DoesUserClubExistsAsync(id, userId))
            {
                return RedirectToAction("Mine", "Club", new { id });
            }
            await clubService.AddToFavoritesAsync(id, userId);
            return RedirectToAction("ShowFavorites");
        }
    }
}
