using System.ComponentModel.DataAnnotations;
using static FootballApp.Common.EntityValidations.LeagueValidations;

namespace FootballApp.Data.Models
{
    public class League
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(CountryMaxLength)]
        public string Country { get; set; } = null!;

        [Required]
        [MaxLength(LogoMaxLength)]
        public string Logo { get; set; } = null!;

        public virtual List<Club> Clubs { get; set; } = new List<Club>();

        public virtual List<Fixture> Fixtures { get; set; } = new List<Fixture>();
    }
}
