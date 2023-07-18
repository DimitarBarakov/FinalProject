using FootballApp.Services.Interfaces;
using FootballApp.ViewModels.Club;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static FootballApp.Common.NotificationMessagesConstants;

namespace FootballApp.Controllers
{
    public class ClubController : Controller
    {
        private readonly IClubService clubService;
        private readonly IUserClubService userClubService;
        private readonly IStadiumService stadiumService;
        private readonly ILeagueService leagueService;

        public ClubController(IClubService service, IUserClubService ucservice, IStadiumService sservice, ILeagueService leagueService)
        {
            this.clubService = service;
            this.userClubService = ucservice;
            this.stadiumService = sservice;
            this.leagueService = leagueService;
        }
        //TODO: Only for admins
        [HttpGet]
        public async Task<IActionResult> AddClub(int id)
        {
            var model = new AddClubViewModel();
            model.Stadiums = await stadiumService.GetStadiumsForAddClubViewModelAsync();
            model.League = await leagueService.GetAddClubLeagueViewModelAsync(id);

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> AddClub(int id, AddClubViewModel model)
        {
            model.League = await leagueService.GetAddClubLeagueViewModelAsync(id);
            if (!ModelState.IsValid)
            {
                model.Stadiums = await stadiumService.GetStadiumsForAddClubViewModelAsync();
                model.League = await leagueService.GetAddClubLeagueViewModelAsync(id);
                return View(model);
            }
            await clubService.AddClubAsync(model);
            return RedirectToAction("ShowById", "League", new {id});
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
            var club = await clubService.GetClubByIdAsync(id);
            if (club == null)
            {
                return BadRequest("Club with this Id does not exists!");
            }
            if (await userClubService.DoesUserClubExistsAsync(id, userId))
            {
                TempData[ErrorMessage] = "This club is already added to your favorites";
                return RedirectToAction("ClubById", "Club", new {id});
            }
            TempData[SuccessMessage] = $"You added {club.Name} to your favorites";
            await clubService.AddToFavoritesAsync(id, userId);
            return RedirectToAction("FavoriteClubs");
        }

        public async Task<IActionResult> FavoriteClubs(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            List<FavoriteCLubsViewModel> model = await userClubService.GetFavoriteClubsAsync(id, userId);

            return View(model);
        }
    }
}
