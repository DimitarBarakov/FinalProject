using FootballApp.ViewModels.Fixture;

namespace FootballApp.ViewModels.League
{
    public class LeaguePageViewModel
    {
        public int Id { get; set; }
        public string Logo { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string Country { get; set; } = null!;

        public List<LeagueClubViewModel> Clubs { get; set; } = new List<LeagueClubViewModel>();

        public List<AllFixturesViewModel> Fixtures { get; set; } = new List<AllFixturesViewModel>();
    }
}
