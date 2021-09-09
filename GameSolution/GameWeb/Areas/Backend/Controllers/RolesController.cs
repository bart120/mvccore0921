using GameWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameWeb.Areas.Backend.Controllers
{
    [Area("Backend")]
    [Authorize]
    public class RolesController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<WebUser> _userManager;

        public RolesController(RoleManager<IdentityRole> roleManager, UserManager<WebUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {

            var role = await _roleManager.FindByNameAsync("ADMIN");
            var user = await _userManager.GetUserAsync(User);

            await _userManager.AddToRoleAsync(user, role.Name);

            return View(_roleManager.Roles.ToList());
        }

        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(IdentityRole model)
        {
            if (ModelState.IsValid)
            {
                await _roleManager.CreateAsync(model);
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
