using FootballApp.Data.Models;
using FootballApp.Services.Interfaces;
using FootballApp.ViewModels.Player;
using Microsoft.AspNetCore.Mvc;

namespace FootballApp.Controllers
{
    public class PlayerController : Controller
    {
        private readonly IPlayerService playerService;

        public PlayerController(IPlayerService service)
        {
            playerService = service;
        }
        public async Task<IActionResult> ShowPlayerById(int id)
        {
            bool doesPlayerExists = await playerService.DoesPlayerExistsByIdAsync(id);


            //TODO: Add custom error page
            if (!doesPlayerExists)
            {
                return BadRequest();
            }

            PlayerPageViewModel? model = await playerService.GetPlayerByIdAsync(id);

            return View(model);
        }
        [HttpGet]
        public IActionResult AddPlayer()
        {
            FormPlayerViewModel model = new FormPlayerViewModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddPlayer(int id, FormPlayerViewModel model)
        {
            model.ClubId = id;
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await playerService.AddPlayerAsync(id, model);
            return RedirectToAction("ClubById", "Club", new {id});
        }

        [HttpGet]
        public async Task<IActionResult> EditPlayer(int id)
        {
            Player? playerToEdit = await playerService.GetPlayerAsync(id);

            FormPlayerViewModel model = new FormPlayerViewModel()
            {
                FirstName = playerToEdit.FirstName,
                LastName = playerToEdit.LastName,
                Age = playerToEdit.Age,
                Number = playerToEdit.Number,
                Goals = playerToEdit.Goals,
                Assists = playerToEdit.Assists,
                ClubId = playerToEdit.ClubId,
                MatchesPlayed = playerToEdit.MatchesPlayed,
                Country = playerToEdit.Country,
                Position = playerToEdit.Position,
                Picture = playerToEdit.Picture
            };

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditPlayer(int id, FormPlayerViewModel model)
        {
            //model.ClubId = id;
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await playerService.EditPlayerAsync(id, model);
            return RedirectToAction("ShowPlayerById", "Player", new { id });
        }
    }
}
