namespace FootballApp.ViewModels.League
{
    public class LeaguePageViewModel
    {
        public string Logo { get; set; } = null!;

        public string Name { get; set; } = null!;

        public List<LeagueClubViewModel> Clubs { get; set; } = new List<LeagueClubViewModel>();
    }
}
