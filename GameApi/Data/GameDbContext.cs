using GameApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameApi.Data
{
    public class GameDbContext : DbContext
    {
        public GameDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Platform>(p => p.HasIndex(x => x.Name).IsUnique());
            modelBuilder.Entity<PlatformGame>(p => p.HasKey(x => new { x.GameID, x.PlatformID }));
            //modelBuilder.Entity<PlatformGame>().HasOne(x => x.Game).WithMany(x => x.PlatformGames).HasForeignKey(x => x.GameID);
            //modelBuilder.Entity<PlatformGame>().HasOne(x => x.Platform).WithMany(x => x.PlatformGames).HasForeignKey(x => x.PlatformID);
        }

        public DbSet<Game> Games { get; set; }

        public DbSet<Platform> Platforms { get; set; }

        public DbSet<PlatformGame> PlatformGames { get; set; }
    }
}
