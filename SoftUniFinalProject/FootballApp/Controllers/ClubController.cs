using FootballApp.Data.Models;
using FootballApp.Services.Interfaces;
using FootballApp.ViewModels.Club;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Configuration;
using System.Security.Claims;
using static FootballApp.Common.NotificationMessagesConstants;
using static FootballApp.Common.GeneralConstants;

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
        [HttpGet]
        [Authorize(Roles = AdminRoleName)]
        public async Task<IActionResult> AddClub(int id)
        {
            var model = new FormClubViewModel();
            model.Stadiums = await stadiumService.GetStadiumsForAddClubViewModelAsync();
            model.League = await leagueService.GetAddClubLeagueViewModelAsync(id);

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = AdminRoleName)]
        public async Task<IActionResult> AddClub(int id, FormClubViewModel model)
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
                return NotFound("Club with this Id does not exists!");
            }
            if (await userClubService.DoesUserClubExistsAsync(id, userId))
            {
                TempData[ErrorMessage] = $"{club.Name} is already added to your favorites";
                return RedirectToAction("ClubById", "Club", new {id});
            }
            TempData[SuccessMessage] = $"You added {club.Name} to your favorites";
            await userClubService.AddToFavoritesAsync(id, userId);
            return RedirectToAction("FavoriteClubs");
        }
        [Authorize]
        public async Task<IActionResult> FavoriteClubs(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            List<FavoriteCLubsViewModel> model = await userClubService.GetFavoriteClubsAsync(id, userId);

            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = AdminRoleName)]
        public async Task<IActionResult> EditClub(int id)
        {
            Club clubToEdit = await clubService.GetClubAsync(id);
            if (clubToEdit == null)
            {
                return NotFound("Club with this id does not exists");
            }
            FormClubViewModel model = new FormClubViewModel() 
            {
                Name = clubToEdit.Name,
                Nickname = clubToEdit.Nickname,
                Logo = clubToEdit.Logo,
                YearOfCreation = clubToEdit.YearOfCreation,
                Wins = clubToEdit.Wins,
                Loses = clubToEdit.Loses,
                Draws = clubToEdit.Draws,
                MatchesPlayed = clubToEdit.MatchesPlayed,
                ScoredGoals = clubToEdit.ScoredGoals,
                ConcededGoals = clubToEdit.ConcededGoals,
                StadiumId = clubToEdit.StadiumId
            };
            model.Stadiums = await stadiumService.GetStadiumsForAddClubViewModelAsync();

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = AdminRoleName)]
        public async Task<IActionResult> EditClub(int id, FormClubViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await clubService.EditClubAsync(id, model);
            return RedirectToAction("ClubById", "Club", new { id });
        }

        [Authorize]
        public async Task<IActionResult> RemoveFromFavorites(int id)
        {
            try
            {

            }
            catch (Exception)
            {

                throw;
            }
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await userClubService.RemoveFromFavorites(id,userId);
            TempData[SuccessMessage] = $"Successfully removed club from favorites";

            return RedirectToAction("FavoriteClubs");
        }
    }
}
