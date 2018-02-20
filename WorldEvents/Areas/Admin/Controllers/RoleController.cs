using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorldEvents.Entities;
using WorldEvents.Models;

namespace WorldEvents.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RoleController : Controller
    {
        private readonly RoleManager<ApplicationRole> roleManager;

        public RoleController(RoleManager<ApplicationRole> roleManager)
        {
            this.roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<ApplicationRoleListViewModel> model = new List<ApplicationRoleListViewModel>();
            model = roleManager.Roles.Select(r => new ApplicationRoleListViewModel
            {
                RoleName = r.Name,
                Id = r.Id,
                Description = r.Description,
                //NumberOfUsers = r.Users.Any() ? r.Users.Count() : 0
            }).ToList();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> AddEditApplicationRole(string id)
        {
            ApplicationRoleViewModel model = new ApplicationRoleViewModel();
            if (!String.IsNullOrEmpty(id))
            {
                var applicationRole = await roleManager.FindByIdAsync(id);
                if (applicationRole != null)
                {
                    model.Id = applicationRole.Id;
                    model.RoleName = applicationRole.Name;
                    model.Description = applicationRole.Description;
                }
            }
            return PartialView("_AddEditApplicationRole", model);
        }

        [HttpPost]
        public async Task<IActionResult> AddEditApplicationRole(ApplicationRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                bool isExist = !String.IsNullOrEmpty(model.Id);
                var applicationRole = isExist ? await roleManager.FindByIdAsync(model.Id) :
                                                   new ApplicationRole
                                                   {
                                                       CreatedDate = DateTime.UtcNow
                                                   };
                applicationRole.Name = model.RoleName;
                applicationRole.Description = model.Description;
                applicationRole.IPAddress = Request.HttpContext.Connection.RemoteIpAddress.ToString();
                IdentityResult result = isExist ? await roleManager.UpdateAsync(applicationRole)
                                                   : await roleManager.CreateAsync(applicationRole);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    var errorsList = string.Join(", \\r\\n", result.Errors.Select(i => i.Description));
                    AddError(errorsList);
                }
            }
            return View("_AddEditApplicationRole", model);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteApplicationRole(string id)
        {
            if (!String.IsNullOrEmpty(id))
            {
                var applicationRole = await roleManager.FindByIdAsync(id);
                var model = applicationRole;
                return PartialView("ConfirmDeleteApplicationRole", model);
            }

            AddError("Cannot delete role is empty");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmDeleteApplicationRole(string id)
        {
            if (!String.IsNullOrEmpty(id))
            {
                var applicationRole = await roleManager.FindByIdAsync(id);
                if (applicationRole != null)
                {
                    IdentityResult roleResult = roleManager.DeleteAsync(applicationRole).Result;
                    if (roleResult.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }
            AddError("Cannot delete role. Id is Empty");
            return View();
        }


        private void AddError(string errorMsg)
        {
            ModelState.AddModelError(string.Empty, errorMsg);
        }
    }
}
