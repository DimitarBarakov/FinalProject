using System.ComponentModel.DataAnnotations;
using static FootballApp.Common.EntityValidations.PlayerValidations;

namespace FootballApp.ViewModels.Player
{
    public class AddPlayerViewModel
    {
        [Required]
        [StringLength(FirstNameMaxLength, MinimumLength = FirstNameMinLength)]
        public string FirstName { get; set; } = null!;

        [Required]
        [StringLength(LastNameMaxLength, MinimumLength = LastNameMinLength)]
        public string LastName { get; set; } = null!;

        [Required]
        [Range(NumberMinValue,NumberMaxValue)]
        public int Number { get; set; }

        [Required]
        [Range(AgeMinValue, AgeMaxValue)]
        public int Age { get; set; }

        [Required]
        [StringLength(CountryMaxLength, MinimumLength = CountryMinLength)]
        public string Country { get; set; } = null!;

        [Required]
        [MaxLength(PictureMaxLength)]
        public string Picture { get; set; } = null!;

        [Required]
        [StringLength(PositionMaxLength, MinimumLength = PositionMinLength)]
        public string Position { get; set; } = null!;

        [Required]
        [Range(GoalsMinCount, int.MaxValue)]
        public int Goals { get; set; }

        [Required]
        [Range(AssistsMinCount, int.MaxValue)]
        public int Assists { get; set; }

        [Required]
        [Range(MatchesPlayedMinCount, int.MaxValue)]
        public int MatchesPlayed { get; set; }

        public int? ClubId { get; set; }
    }
}
