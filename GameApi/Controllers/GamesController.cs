using GameApi.Data;
using GameApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace GameApi.Controllers
{
    /// <summary>
    /// API for games
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly GameDbContext _context;

        public GamesController(GameDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Game> GetGames()
        {
            return _context.Games.ToList();

            /* _context.Games.Where(x => x.Name.Contains("i"));
             var r = from x in _context.Games
                     where x.Name.Contains("i")
                     select x;


             List<string> liste = new List<string>();
             liste.Add("Florian");
             liste.Add("Vincent");


             var result = liste.Where(x => x.Contains("i"));

             var result2 = from x in liste
                          where x.Contains("i")
                          select x;
            */
        }

        /// <summary>
        /// Get a game by id
        /// </summary>
        /// <param name="id">key of the game</param>
        /// <returns>A game</returns>
        /// <example>https://sample.com/api/games/5</example>
        /// <seealso cref="GetGames"/>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(Game), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        //[Consumes("text/xml")]
        public async Task<ActionResult<Object>> GetGameByIdAsync([FromRoute] int id)
        {
            //var game = await _context.Games.Include(x => x.PlatformGames).ThenInclude(x => x.Platform).Select(x => new { x.ID, x.Name, x.Description, Platforms = x.PlatformGames.Select(y => new { y.PlatformID, y.Platform.Name }) }).SingleOrDefaultAsync(x => x.ID == id);
            var game = await _context.Games.FindAsync(id);
            if (game != null)
            {
                //return new ContentResult{ Content = "", ContentType = "text/xml", StatusCode= (int)HttpStatusCode.OK };
                return game;
            }
            else
            {
                return NotFound("No game for this ID");
            }

        }

        [HttpGet("{id:int}/platforms")]
        [ProducesResponseType(typeof(Game), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<IEnumerable<Platform>>> GetPLatformsByGameIdAsync([FromRoute] int id)
        {
            //var game = await _context.Games.Include(x => x.PlatformGames).ThenInclude(x => x.Platform).Select(x => new { x.ID, x.Name, x.Description, Platforms = x.PlatformGames.Select(y => new { y.PlatformID, y.Platform.Name }) }).SingleOrDefaultAsync(x => x.ID == id);
            var platforms = await _context.PlatformGames.Include(x => x.Platform).Where(x => x.GameID == id).Select(x => x.Platform).ToListAsync();
            if (platforms != null && platforms.Count() > 0)
            {
                //return new ContentResult{ Content = "", ContentType = "text/xml", StatusCode= (int)HttpStatusCode.OK };
                return platforms;
            }
            else
            {
                return NotFound("No platforms for this game");
            }

        }

        /// <summary>
        /// Insert a game in DB
        /// </summary>
        /// <param name="game"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(Game), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public ActionResult<Game> PostGame([FromBody] Game game)
        {
            if (ModelState.IsValid)
            {
                _context.Games.Add(game);

                _context.SaveChanges();
                return Created("api/games", game);
            }
            else
                return BadRequest(ModelState);

            /*var trans = _context.Database.BeginTransaction();
            try
            {
                trans.Commit();
            }
            catch (Exception)
            {
                trans.Rollback();
            }*/
        }

        [HttpPut("{id:int}/editplatform")]
        [ProducesResponseType(typeof(Game), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<Game>> PutPlatformGame([FromBody] List<int> model, [FromRoute] int id)
        {
            try
            {
                var platforms = await _context.PlatformGames.Where(x => x.GameID == id).ToListAsync();

                var adds = model.Where(x => !platforms.Any(y => y.PlatformID == x));
                var dels = platforms.Where(x => !model.Any(y => y == x.PlatformID));

                _context.PlatformGames.RemoveRange(dels);
                foreach (var item in adds)
                {
                    await _context.PlatformGames.AddAsync(new PlatformGame { GameID = id, PlatformID = item });
                }

                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> PutGame([FromRoute] int id, [FromBody] Game game)
        {
            if (id != game.ID)
            {
                return BadRequest();
            }
            _context.Entry(game).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GameExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        private bool GameExists(int id)
        {
            return _context.Games.Any(e => e.ID == id);
        }

    }
}
