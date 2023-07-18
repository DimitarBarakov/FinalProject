using System.ComponentModel.DataAnnotations;
using static FootballApp.Common.EntityValidations.PlayerValidations;

namespace FootballApp.Data.Models
{
    public class Position
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(PositionMaxLength)]
        public string Name { get; set; } = null!;
        public List<Player> Players { get; set; } = new List<Player>();

    }
}
