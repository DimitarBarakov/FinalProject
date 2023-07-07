﻿using FootballApp.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FootballApp.Data
{
    public class FootballAppDbContext : IdentityDbContext
    {
        public FootballAppDbContext(DbContextOptions<FootballAppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Player> Players { get; set; }
        public DbSet<Club> Clubs { get; set; }
        public DbSet<League> Leagues { get; set; }
        public DbSet<Stadium> Stadiums { get; set; }
        public DbSet<Fixture> Fixtures { get; set; }
        public DbSet<UserClub> FavoriteClubs { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UserClub>()
                .HasKey(k => new {k.UserId, k.ClubId});

            builder.Entity<Club>()
                .HasMany(c => c.AwayFixtures)
                .WithOne(f => f.AwayClub)
                .HasForeignKey(f => f.AwayClubId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Club>()
                .HasMany(c => c.HomeFixtures)
                .WithOne(f => f.HomeClub)
                .HasForeignKey(f => f.HomeClubId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }
    }
}