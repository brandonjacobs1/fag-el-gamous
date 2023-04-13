using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using fag_el_gamous.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace fag_el_gamous.Controllers
{

    public class AdminController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        //private readonly IUserRoleStore<IdentityUser> _userRoleManager;

        public AdminController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
            //IUserRoleStore<IdentityUser> userRoleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            //_userRoleManager = userRoleManager;
        }


        // GET: /<controller>/
        [Authorize(Roles = "Admin")]
        public IActionResult Admin()
        {
            var users = _userManager.Users.ToList();
            var roles = _roleManager.Roles.ToList();

            var viewModel = new Application
            {
                Users = users,
                Roles = roles
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddRole(Application model)
        {
            if (!string.IsNullOrEmpty(model.NewRole))
            {
                var newRole = new IdentityRole
                {
                    Name = model.NewRole
                };

                var result = await _roleManager.CreateAsync(newRole);

                if (result.Succeeded)
                {
                    return RedirectToAction("Admin");
                }
                else
                {
                    ModelState.AddModelError("", "failed to add role");
                }
            }

            return RedirectToAction("Admin");
        }


        [HttpPost]
        public async Task<IActionResult> AddRoleToUser(string userId, string roleName)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(userId);
                var role = await _roleManager.FindByNameAsync(roleName);
                if (user != null && role != null)
                {
                    await _userManager.AddToRoleAsync(user, role.Name);
                    return RedirectToAction("Admin");
                }
                else
                {
                    ModelState.AddModelError("", "user not found");
                }
            }
            return RedirectToAction("Admin");
        }
    }
}


