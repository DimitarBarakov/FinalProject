namespace FootballApp.ViewModels.Fixture
{
    public class AllFixturesViewModel
    {
        public int Id { get; set; }
        public string StartTime { get; set; } = null!;
        public FixtureClubViewModel HomeClub { get; set; } = null!;
        public FixtureClubViewModel AwayClub { get; set; } = null!;
        public FixtureStadiumViewModel Stadium { get; set; } = null!;
    }
}
