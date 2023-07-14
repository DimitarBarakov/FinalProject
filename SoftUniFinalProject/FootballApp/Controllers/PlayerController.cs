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
    }
}
