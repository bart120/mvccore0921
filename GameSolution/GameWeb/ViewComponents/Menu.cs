using GameLib.Core.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameWeb.ViewComponents
{
    public class Menu : ViewComponent
    {
        private readonly IPlatformService _platformService;
        public Menu(IPlatformService platformService)
        {
            _platformService = platformService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var platforms = await _platformService.GetPlatformsAsync();
            return View(platforms);
        }
    }
}
