using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using static FootballApp.Common.EntityValidations.ClubValidations;

namespace FootballApp.Data.Models
{
    public class Club
    {

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(LogoMaxLength)]
        public string Logo { get; set; } = null!;

        [Required]
        [MaxLength(NicknameMaxLength)]
        public string Nickname { get; set; } = null!;

        [Required]
        public int YearOfCreation { get; set; }

        [Required]
        public int Wins { get; set; }

        [Required]
        public int Draws { get; set; }

        [Required]
        public int Loses { get; set; }

        public int Points => this.Wins * 3 + this.Draws;

        [Required]
        public int MatchesPlayed { get; set; }

        [Required]
        public int ScoredGoals { get; set; }

        [Required]
        public int ConcededGoals { get; set; }

        public int GoalDifference => this.ScoredGoals - this.ConcededGoals;

        public bool IsActive { get; set; }

        [Required]
        [ForeignKey(nameof(League))]
        public int LeagueId { get; set; }

        [Required]
        public virtual League League { get; set; } = null!;

        public virtual List<Player> Players { get; set; } = new List<Player>();

        [Required]
        [ForeignKey(nameof(Stadium))]
        public int StadiumId { get; set; }

        public virtual Stadium Stadium { get; set; } = null!;

        public virtual List<Fixture> HomeFixtures  { get; set; } = new List<Fixture>();
        public virtual List<Fixture> AwayFixtures { get; set; } = new List<Fixture>();
    }
}
