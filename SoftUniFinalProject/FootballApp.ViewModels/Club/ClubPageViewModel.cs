using FootballApp.Data.Models;

namespace FootballApp.ViewModels.Club
{
    public class ClubPageViewModel
    {
        public string Logo { get; set; } = null!;
        public string Name { get; set; } = null!;

        public string? Nickname { get; set; }

        public Stadium Stadium { get; set; } = null!;

        public List<ClubPagePlayerViewModel> Players { get; set; } = new List<ClubPagePlayerViewModel>();
    }
}
