using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static FootballApp.Common.EntityValidations.PlayerValidations;

namespace FootballApp.Data.Models
{
    public class Player
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(FirstNameMaxLength)]
        public string FirstName { get; set; } = null!;

        [Required]
        [MaxLength(LastNameMaxLength)]
        public string LastName { get; set; } = null!;

        [Required]
        public int Number { get; set; }

        [Required]
        public int Age { get; set; }

        [Required]
        [MaxLength(CountryMaxLength)]
        public string Country { get; set; } = null!;

        [Required]
        [MaxLength(PictureMaxLength)]
        public string Picture { get; set; } = null!;

        [Required]
        [MaxLength(PositionMaxLength)]
        public string Position { get; set; } = null!;

        [Required]
        public int Goals { get; set; }

        [Required]
        public int Assists { get; set; }

        [Required]
        public int MatchesPlayed { get; set; }

        public bool IsActive { get; set; }

        [Required]
        [ForeignKey(nameof(Club))]
        public int ClubId { get; set; }

        [Required]
        public virtual Club Club { get; set; } = null!;
    }
}