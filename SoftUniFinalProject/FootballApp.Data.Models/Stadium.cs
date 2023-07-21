using System.ComponentModel.DataAnnotations;
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

        [Required]
        public string Location { get; set; } = null!;

        [Required]
        public int Capacity { get; set; }

        public virtual List<Club> Clubs { get; set; } = new List<Club>();
    }
}