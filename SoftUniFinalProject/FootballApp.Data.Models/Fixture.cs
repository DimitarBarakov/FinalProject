﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FootballApp.Data.Models
{
    public class Fixture
    {

        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public virtual Stadium Stadium => this.HomeClub.Stadium;

        [Required]
        [ForeignKey(nameof(HomeClub))]
        public int HomeClubId { get; set; }

        [Required]
        public virtual Club HomeClub { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(AwayClub))]
        public int AwayClubId { get; set; }

        [Required]
        public virtual Club AwayClub { get; set; } = null!;

        public bool IsActive { get; set; }

        [Required]
        public int LeagueId { get; set; }

        [Required]
        public virtual League League { get; set; } = null!;
    }
}
