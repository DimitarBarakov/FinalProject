using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FootballApp.Data.Models
{
    public class UserClub
    {
        [Required]
        [ForeignKey(nameof(Club))]
        public int ClubId { get; set; }
        public virtual Club Club { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(User))]
        public string UserId { get; set; } = null!;
        public IdentityUser User { get; set; } = null!;
    }
}
