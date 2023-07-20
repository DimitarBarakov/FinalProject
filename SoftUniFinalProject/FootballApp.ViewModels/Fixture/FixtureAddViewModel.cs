namespace FootballApp.ViewModels.Fixture
{
    public class FixtureAddViewModel
    {
        public int HomeClubId { get; set; }
        public int AwayClubId { get; set; }
        public DateTime StartTime { get; set; }
        public int LeagueId { get; set; }

        public virtual List<AddFixtureClubViewModel> Clubs { get; set; } = new List<AddFixtureClubViewModel>();

        public virtual List<AddFixtureLeagueViewModel> Leagues { get; set; } = new List<AddFixtureLeagueViewModel>();
    }
}
