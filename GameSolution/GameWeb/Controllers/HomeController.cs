using GameLib.Core.Services;
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

        public HomeController(IPlatformService platformService)
        {
            _platformService = platformService;
        }


        public async Task<IActionResult> Index()
        {
            var platforms = await _platformService.GetPlatformsAsync();
            return View();
        }

        [Route("a-propos-de", Name = "RouteAbout")]
        //[Route("about")]
        public IActionResult About()
        {
            
            return View();
        }
    }
}
