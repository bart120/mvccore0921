using GameLib.Core.Services;
using GameWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPlatformService _platformService;
        private readonly IGameService _gameService;

        public HomeController(IPlatformService platformService, IGameService gameService)
        {
            _platformService = platformService;
            _gameService = gameService;
        }


        public async Task<IActionResult> Index()
        {
            var model = new HomeIndexViewModel();

            model.Platforms = await _platformService.GetPlatformsAsync();
            model.Games = await _gameService.GetGamesAsync();
            //ViewData["Title"] = "Accueil";
            ViewBag.Title = "Accueil";
            
            return View(model);
        }

        [Route("a-propos-de", Name = "RouteAbout")]
        //[Route("about")]
        public IActionResult About()
        {
            
            return View();
        }
    }
}
