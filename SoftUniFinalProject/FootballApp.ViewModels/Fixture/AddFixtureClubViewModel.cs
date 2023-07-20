namespace FootballApp.ViewModels.Fixture
{
    public class AddFixtureClubViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public int LeagueId { get; set; }
    }
}