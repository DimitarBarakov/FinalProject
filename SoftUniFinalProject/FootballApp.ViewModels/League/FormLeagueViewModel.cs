using System.ComponentModel.DataAnnotations;
using static FootballApp.Common.EntityValidations.LeagueValidations;

namespace FootballApp.ViewModels.League
{
    public class FormLeagueViewModel
    {
        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(CountryMaxLength, MinimumLength = CountryMinLength)]
        public string Country { get; set; } = null!;

        [Required]
        [MaxLength(LogoMaxLength)]
        public string Logo { get; set; } = null!;
    }
}
