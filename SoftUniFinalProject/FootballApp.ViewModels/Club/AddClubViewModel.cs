using FootballApp.Data.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using static FootballApp.Common.EntityValidations.ClubValidations;

namespace FootballApp.ViewModels.Club
{
    public class AddClubViewModel
    {
        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(LogoMaxLength)]
        public string Logo { get; set; } = null!;

        [Required]
        [StringLength(NicknameMaxLength, MinimumLength = NicknameMinLength)]
        public string Nickname { get; set; } = null!;

        [Required]
        [Range(MinYear, MaxYear)]
        public int YearOfCreation { get; set; }

        [Required]
        [Range(LosesMinCount,int.MaxValue)]
        public int Wins { get; set; }

        [Required]
        [Range(DrawsMinCount, int.MaxValue)]
        public int Draws { get; set; }

        [Required]
        [Range(WinsMinCount, int.MaxValue)]
        public int Loses { get; set; }

        [Required]
        [Range(MatchesPlayedMinCount, int.MaxValue)]
        public int MatchesPlayed { get; set; }

        [Required]
        [Range(ScoredGoalMinCount, int.MaxValue)]
        public int ScoredGoals { get; set; }

        [Required]
        [Range(ConcededGoalsMinCount, int.MaxValue)]
        public int ConcededGoals { get; set; }

        [Required]
        public int LeagueId { get; set; }

        [Required]
        public int StadiumId { get; set; }
    }
}
