using GameApi.Controllers;
using GameApi.Data;
using GameApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace GameApi.Test
{
    public class UnitTestPlatformController
    {
        private readonly GameDbContext _context;
        public UnitTestPlatformController()
        {
            var optionsBuilder = new DbContextOptionsBuilder<GameDbContext>();
            optionsBuilder.UseInMemoryDatabase("test");
            _context = new GameDbContext(optionsBuilder.Options);
            _context.Platforms.Add(new Platform { Name = "Platform 1", Description = "" });
            _context.Platforms.Add(new Platform { Name = "Platform 2", Description = "" });
            _context.SaveChanges();
        }

        [Fact]
        public async Task TestGetPlatforms()
        {
            var controller = new PlatformsController(_context);
            var result = (await controller.GetPlatforms()).Value;

            Assert.Equal(2, result.Count());

        }
    }
}
