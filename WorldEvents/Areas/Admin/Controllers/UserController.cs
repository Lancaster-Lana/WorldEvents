﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WorldEvents.Entities;
using WorldEvents.Models;

namespace WorldEvents.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<ApplicationRole> roleManager;

        public UserController(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<UserListViewModel> model = new List<UserListViewModel>();
            model = userManager.Users.Select(u => new UserListViewModel
            {
                Id = u.Id,
                Name = u.UserName,
                Email = u.Email
            }).ToList();

            return View(model);
        }

        [HttpGet]
        public IActionResult AddUser()
        {
            UserViewModel model = new UserViewModel();
            model.ApplicationRoles = roleManager.Roles.ToList();
            return PartialView(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    NormalizedUserName = model.Name,
                    UserName = model.UserName,
                    Email = model.Email
                };
                IdentityResult result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    ApplicationRole applicationRole = await roleManager.FindByIdAsync(model.ApplicationRoleId);
                    if (applicationRole != null)
                    {
                        IdentityResult roleResult = await userManager.AddToRoleAsync(user, applicationRole.Name);
                        if (roleResult.Succeeded)
                        {
                            return RedirectToAction("Index");
                        }
                    }
                }
                else
                {
                    var errorsList = string.Join(", ", result.Errors.Select(i => i.Description) );
                    AddError(errorsList);
                    //throw new Exception(errorsList);
                }

            }
            return View(model);//with errors
        }

        [HttpGet]
        public async Task<IActionResult> EditUser(string id)
        {
            EditUserViewModel model = new EditUserViewModel();
            model.ApplicationRoles = roleManager.Roles.ToList();

            if (!String.IsNullOrEmpty(id))
            {
                ApplicationUser user = await userManager.FindByIdAsync(id);
                if (user != null)
                {
                    model.Id = user.Id;
                    model.Name = user.UserName;
                    model.Email = user.Email;

                    var userRoles = await userManager.GetRolesAsync(user);
                    if (userRoles.Any()) //model.ApplicationRoleId =user.RoleId
                        model.ApplicationRoleId = roleManager.Roles.Single(r => r.Name == userRoles.FirstOrDefault()).Id;
                }
            }
            return PartialView(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = await userManager.FindByIdAsync(model.Id);
                if (user != null)
                {
                    user.UserName = model.Name;
                    user.Email = model.Email;
                    var existingUserRoles = await userManager.GetRolesAsync(user);

                    IdentityResult result = await userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        //Remove old user role
                        if (existingUserRoles.Any())
                        {
                            string existingRoleName = existingUserRoles.FirstOrDefault();
                            string existingRoleId = roleManager.Roles.Single(r => r.Name == existingRoleName).Id;

                            if (existingRoleId != model.ApplicationRoleId)
                            {
                                IdentityResult roleResult = await userManager.RemoveFromRoleAsync(user, existingRoleName);
                            }
                        }

                        //Update role of the user //if (roleResult.Succeeded)
                        ApplicationRole applicationRole = await roleManager.FindByIdAsync(model.ApplicationRoleId);
                        if (applicationRole != null)
                        {
                            IdentityResult newRoleResult = await userManager.AddToRoleAsync(user, applicationRole.Name);
                            if (newRoleResult.Succeeded)
                            {
                                return RedirectToAction("Index");
                            }
                        }

                    }
                }
            }
            return View(model); //with errors
        }

        [HttpGet]
        public async Task<IActionResult> DeleteUser(string id)
        {
            string name = string.Empty;
            if (!String.IsNullOrEmpty(id))
            {
                ApplicationUser applicationUser = await userManager.FindByIdAsync(id);
                return PartialView("ConfirmDeleteUser", applicationUser);
            }

           AddError("User id is empty");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmDeleteUser(string id)
        {
            if (!String.IsNullOrEmpty(id))
            {
                ApplicationUser applicationUser = await userManager.FindByIdAsync(id);
                if (applicationUser != null)
                {
                    IdentityResult result = await userManager.DeleteAsync(applicationUser);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }

            AddError("Cannot delete user by empty id ");
            return View();
        }


        private void AddError(string errorMsg)
        {
            ModelState.AddModelError(string.Empty, errorMsg);
        }
    }
}
