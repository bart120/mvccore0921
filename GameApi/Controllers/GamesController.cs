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
        public async Task<ActionResult<Object>> GetGameByIdAsync([FromRoute]int id)
        {
            var game = await _context.Games.Include(x => x.PlatformGames).ThenInclude(x => x.Platform).Select(x => new { x.ID, x.Name, x.Description, Platforms = x.PlatformGames.Select(y => new { y.PlatformID, y.Platform.Name }) }).SingleOrDefaultAsync(x => x.ID == id);
            if(game != null)
            {
                //return new ContentResult{ Content = "", ContentType = "text/xml", StatusCode= (int)HttpStatusCode.OK };
                return game;
            }
            else
            {
                return NotFound("No game for this ID");
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
        public ActionResult<Game> PostGame([FromBody]Game game)
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


    }
}
