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
            PlayerPageViewModel? model = await playerService.GetPlayerByIdAsync(id);

            return View(model);
        }
    }
}
