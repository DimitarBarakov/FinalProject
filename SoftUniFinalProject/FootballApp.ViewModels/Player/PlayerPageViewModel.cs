namespace FootballApp.ViewModels.Player
{
    public class PlayerPageViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Picture { get; set; } = null!;
        public string Country { get; set; } = null!;
        public string Position { get; set; } = null!;
        public int Age { get; set; }
        public int Nmber { get; set; }
        public int Goals{ get; set; }
        public int Assists { get; set; }
        public int MatchesPlayed { get; set; }
        public PlayerPageClubViewModel Club { get; set; } = null!;
    }
}
