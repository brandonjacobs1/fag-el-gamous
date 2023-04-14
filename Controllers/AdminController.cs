using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using fag_el_gamous.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace fag_el_gamous.Controllers
{

    public class AdminController : Controller
    {
        // Set up variables of types UserManager and RoleManager
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }


        // GET: /<controller>/
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Admin()
        {
            var users = _userManager.Users.ToList(); // Get all the users
            var viewModel = new List<UserRolesView>(); 

            foreach (var user in users)
            {
                var userRoles = await _userManager.GetRolesAsync(user); // Get roles that belong to a User
                var userRoleViewModel = new UserRolesView
                {
                    User = user,
                    Roles = userRoles
                };
                viewModel.Add(userRoleViewModel); // Add a UserRoleView to the list
            }

            return View(viewModel); // Pass the list of UserRoleView to the View
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ManageUsers()
        {
            var users = _userManager.Users.ToList(); // Get list of all Users
            
            var viewModel = new UserRolesView
            {
                ListUsers = users
            };

            return View(viewModel); // Pass the list to the View
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ManageRoles()
        {
            var roles = _roleManager.Roles.ToList(); // Get list of all Roles
            
            var viewModel = new UserRolesView
            {
                ListRoles = roles
            };

            return View(viewModel); // Pass the list to the View
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Edit(string id) // Accepts id as a parameter
        {
            if (id == null || _userManager.Users == null) //check to see if null
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id); // find the user with the passed in id

            var roles = _roleManager.Roles.ToList(); // get a list of all roles

            var userRoles = await _userManager.GetRolesAsync(user); // get a list of roles that the user belongs to

            var newRoles = roles.Where(role => !userRoles.Contains(role.Name)).ToList(); // filter the roles down to roles the user does not currently have
            

            var viewModel = new UserRolesView // pass all the needed info into the model variable
            {
                Roles = userRoles,
                User = user,
                ListRoles = newRoles
            };

            if (user == null)
            {
                return NotFound();
            }
            return View(viewModel);
        }

        // Actions beyond this point are pretty self explanatory
        // I re-use most of the same methods i explained above

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Delete(string? id, string? roleName)
        {
            if (id == null && _userManager.Users == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);

            var role = await _roleManager.FindByNameAsync(roleName);

            var viewModel = new UserRolesView
            {
                User = user,
                NewRole = roleName,
                SingleRole = role
            };

            return View(viewModel);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> ConfirmRoleDelete(string roleName)
        {
            if (roleName == null)
            {
                return NotFound();
            }

            var role = await _roleManager.FindByNameAsync(roleName);

            var viewModel = new UserRolesView
            {
                SingleRole = role
            };

            return View(viewModel);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> ConfirmUserDelete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);

            var viewModel = new UserRolesView
            {
                User = user
            };

            return View(viewModel);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> EditUser(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);

            var viewModel = new UserRolesView
            {
                User = user
            };

            return View(viewModel);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> EditUser(string id, string newEmail)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);

            var token = await _userManager.GenerateChangeEmailTokenAsync(user, newEmail);

            await _userManager.ChangeEmailAsync(user, newEmail, token);

            var viewModel = new UserRolesView
            {
                User = user
            };

            
            return RedirectToAction("ManageUsers");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> DeleteUser(string id)
        {
            if (id != null)
            {
                var user = await _userManager.FindByIdAsync(id);
                var result = await _userManager.DeleteAsync(user);

                if (result.Succeeded)
                {
                    return RedirectToAction("ManageUsers");
                }
                else
                {
                    ModelState.AddModelError("", "failed to add role");
                }
            }

            return RedirectToAction("ManageUsers");
        }



        [Authorize(Roles = "Admin")]
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
                    return RedirectToAction("ManageRoles");
                }
                else
                {
                    ModelState.AddModelError("", "failed to add role");
                }
            }

            return RedirectToAction("ManageRoles");
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> DeleteRole(string roleId)
        {
            if (roleId != null)
            {
                var role = await _roleManager.FindByIdAsync(roleId);

                var result = await _roleManager.DeleteAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("ManageRoles");
                }
                else
                {
                    ModelState.AddModelError("", "failed to add role");
                }
            }

            return RedirectToAction("ManageRoles");
        }

        [Authorize(Roles = "Admin")]
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


        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> RemoveRoleFromUser(string userId, string roleName)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(userId);
                var role = await _roleManager.FindByNameAsync(roleName);
                if (user != null && role != null)
                {
                    await _userManager.RemoveFromRoleAsync(user, role.Name);
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


