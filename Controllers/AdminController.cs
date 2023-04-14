﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using fag_el_gamous.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace fag_el_gamous.Controllers
{

    public class AdminController : Controller
    {
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
            var users = _userManager.Users.ToList();
            var viewModel = new List<UserRolesView>(); 

            foreach (var user in users)
            {
                var userRoles = await _userManager.GetRolesAsync(user); 
                var userRoleViewModel = new UserRolesView
                {
                    User = user,
                    Roles = userRoles
                };
                viewModel.Add(userRoleViewModel); 
            }

            return View(viewModel); 
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ManageUsers()
        {
            //var users = _userManager.Users.ToList();
            var users = _userManager.Users.ToList();
            //var viewModel = new List<UserRolesView>();

            //foreach (var user in users)
            //{
            //    var userRoles = await _userManager.GetRolesAsync(user);
            //    var userRoleViewModel = new UserRolesView
            //    {
            //        User = user,
            //        Roles = userRoles
            //    };
            //    viewModel.Add(userRoleViewModel);
            var viewModel = new UserRolesView
            {
                ListUsers = users
            };

            return View(viewModel);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ManageRoles()
        {
            //var users = _userManager.Users.ToList();
            var roles = _roleManager.Roles.ToList();
            //var viewModel = new List<UserRolesView>();

            //foreach (var user in users)
            //{
            //    var userRoles = await _userManager.GetRolesAsync(user);
            //    var userRoleViewModel = new UserRolesView
            //    {
            //        User = user,
            //        Roles = userRoles
            //    };
            //    viewModel.Add(userRoleViewModel);
            var viewModel = new UserRolesView
            {
                ListRoles = roles
            };

            return View(viewModel);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _userManager.Users == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);
            var roles = _roleManager.Roles.ToList();
            var userRoles = await _userManager.GetRolesAsync(user);
            var newRoles = roles.Where(role => !userRoles.Contains(role.Name)).ToList();
            

            var viewModel = new UserRolesView
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
            //var role = _roleManager.FindByNameAsync(roleName);
            //var userRoles = await _userManager.GetRolesAsync(user); // Get the roles for the current user


            var viewModel = new UserRolesView
            {
                User = user,
                NewRole = roleName,
                SingleRole = role
            };

            //if (user == null)
            //{
            //    return NotFound();
            //}
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
            //var role = _roleManager.FindByNameAsync(roleName);
            //var userRoles = await _userManager.GetRolesAsync(user); // Get the roles for the current user


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
            //var role = _roleManager.FindByNameAsync(roleName);
            //var userRoles = await _userManager.GetRolesAsync(user); // Get the roles for the current user


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

            //var role = _roleManager.FindByNameAsync(roleName);
            //var userRoles = await _userManager.GetRolesAsync(user); // Get the roles for the current user


            var viewModel = new UserRolesView
            {
                User = user
            };

            //if (user == null)
            //{
            //    return NotFound();
            //}
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

            //var role = _roleManager.FindByNameAsync(roleName);
            //var userRoles = await _userManager.GetRolesAsync(user); // Get the roles for the current user


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
                //var role = new IdentityRole
                //{
                //    Name = model.
                //};
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
                //var role = new IdentityRole
                //{
                //    Name = model.
                //};
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


