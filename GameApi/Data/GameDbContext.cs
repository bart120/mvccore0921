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
        }

        public DbSet<Game> Games { get; set; }

        public DbSet<Platform> Platforms { get; set; }
    }
}
