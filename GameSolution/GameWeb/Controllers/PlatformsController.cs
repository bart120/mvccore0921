using GameLib.Core.Services;
using GameWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameWeb.Controllers
{
    public class PlatformsController : Controller
    {
        private readonly IPlatformService _platformService;

        public PlatformsController(IPlatformService platformService)
        {
            _platformService = platformService;
        }

        [Route("plateforme-{name}/{id:int}", Name = "RoutePlatform")]
        public async Task<IActionResult> Index([FromRoute]int id, [FromRoute] string name)
        {
            var model = new PlatformsIndexViewModel();
            model.Platform = await _platformService.GetPlatformByIdAsync(id);
            model.Games = await _platformService.GetGameByPlatformsAsync(id);

            return View(model);
        }
    }
}
