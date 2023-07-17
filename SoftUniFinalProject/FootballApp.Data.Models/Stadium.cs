using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static FootballApp.Common.EntityValidations.StadiumValidations;

namespace FootballApp.Data.Models
{
    public class Stadium
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
        [MaxLength(CityMaxLength)]
        public string City { get; set; } = null!;

        [Required]
        [MaxLength(AddressMaxLength)]
        public string Address { get; set; } = null!;

        public virtual List<Club> Clubs { get; set; } = new List<Club>();
    }
}