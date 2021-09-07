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

        public DbSet<Game> Games { get; set; }
    }
}
