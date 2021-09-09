using GameLib.Core.Models;
using GameLib.Core.Services;
using GameWeb.Areas.Backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace GameWeb.Areas.Backend.Controllers
{
    [Area("Backend")]
    [Route("[area]/games/[action]")]
    [Authorize]

    public class BackGamesController : Controller
    {
        private readonly IPlatformService _platformService;
        private readonly IGameService _gameService;

        public BackGamesController(IPlatformService platformService, IGameService gameService)
        {
            _platformService = platformService;
            _gameService = gameService;
        }


        // GET: BackGamesController
        public async Task<ActionResult> Index()
        {
            var games = await _gameService.GetGamesAsync();
            return View(games);
        }

        // GET: BackGamesController/Details/5
        [HttpGet("{id}")]
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: BackGamesController/Create
        [Authorize(Roles = "ADMIN,USER")]
        public async Task<ActionResult> Create()
        {
            ViewBag.Platforms =  await  _platformService.GetPlatformsAsync();
            return View();
        }

        // POST: BackGamesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult> Create(BackGamesCreateViewModel model)
        {
            if(ModelState.IsValid)
            {
                var game = await _gameService.CreateGameAsync(model as Game);
                await _gameService.EditPlatformAsync(model.Platforms, game.ID);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ViewBag.Platforms = await _platformService.GetPlatformsAsync();
                return View();
            }
        }

        // GET: BackGamesController/Edit/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Edit(int id)
        {
            BackGamesEditViewModel game = BackGamesEditViewModel.Convert(await _gameService.GetGameByIdAsync(id));
            game.Platforms = (await _gameService.GetPlatformsByGameIdAsync(id))?.Select(x => x.ID).ToArray();
            ViewBag.Platforms = await _platformService.GetPlatformsAsync();
            return View(game);
        }

        // POST: BackGamesController/Edit/5
        [HttpPost("{id}")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, BackGamesEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var game = await _gameService.EditGameAsync(model as Game);
                await _gameService.EditPlatformAsync(model.Platforms, game.ID);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ViewBag.Platforms = await _platformService.GetPlatformsAsync();
                return View();
            }
        }

        // GET: BackGamesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: BackGamesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
