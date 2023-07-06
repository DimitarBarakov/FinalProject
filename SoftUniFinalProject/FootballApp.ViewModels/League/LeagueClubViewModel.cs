using System.ComponentModel.DataAnnotations;

namespace FootballApp.ViewModels.League
{
    public class LeagueClubViewModel
    {
        public int Id { get; set; }
        public string Logo { get; set; } = null!;
        public string Name { get; set; } = null!;
        [Display(Name = "GD")]
        public int GoalDifferrence { get; set; }
        public int Points { get; set; }
        [Display(Name = "W")]
        public int Wins { get; set; }
        [Display(Name = "D")]
        public int Draws { get; set; }
        [Display(Name = "L")]
        public int Loses { get; set; }
        [Display(Name="MP")]
        public int MathesPlayed{ get; set; }
    }
}
