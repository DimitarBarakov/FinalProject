using FootballApp.Data.Models;
using FootballApp.ViewModels.Fixture;

namespace FootballApp.ViewModels.Club
{
    public class ClubPageViewModel
    {
        public int Id { get; set; }
        public string Logo { get; set; } = null!;
        public string Name { get; set; } = null!;

        public string? Nickname { get; set; }

        public ClubPageStadiumViewModel Stadium{ get; set; } = null!;

        public List<ClubPagePlayerViewModel> Players { get; set; } = new List<ClubPagePlayerViewModel>();
        public List<AllFixturesViewModel> Fixtures { get; set; } = new List<AllFixturesViewModel>();
    }
}
