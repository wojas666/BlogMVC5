using Blog.Domain.Concrete;
using Blog.Domain.Entity;
using Blog.WebUI.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using static System.Net.WebRequestMethods;

namespace Blog.WebUI.Controllers
{
    public class RoleAdminController : Controller
    {
        [Authorize(Roles = "Administrator")]
        public ActionResult Index()
        {
            var roleManager = HttpContext.GetOwinContext().GetUserManager<RoleManager<AppRole>>();
            return View(roleManager.Roles);
        }

        // GET: RoleAdmin
        [Authorize(Roles = "Administrator")]
        public ViewResult CreateNewRole()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateNewRole(CreateRoleViewModel model, String returnUrl)
        {
            if (ModelState.IsValid)
            {
                AppRole role = new AppRole() { Name = model.RoleName };

                var roleManager = HttpContext.GetOwinContext().GetUserManager<RoleManager<AppRole>>();

                if (!roleManager.RoleExists(model.RoleName))
                {
                    roleManager.Create(role);
                    return Redirect(returnUrl ?? Url.Action("Index", "RoleAdmin"));
                }
                else
                {
                    ModelState.AddModelError("", "Podana nazwa jest nieprawidłowa bądź podana rola już istnieje.");
                    return View(model);
                }
            }
            else
            {
                return View();
            }
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(string roleID)
        {
            IEnumerable<User> members;

            var userManager = HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
            var roleManager = HttpContext.GetOwinContext().GetUserManager<RoleManager<AppRole>>();
            AppRole role = roleManager.FindById(roleID);
            
            string[] memberIds = role.Users.Select(e => e.UserId).ToArray();
            members = userManager.Users.Where(x => memberIds.Any(y => y == x.Id));
            

            IEnumerable<User> nonMembers = userManager.Users.Except(members);

            return View(new EditRoleViewModel()
            {
                Role = role,
                Member = members,
                NonMember = nonMembers
            });
        }

        [HttpPost]
        public async Task<ActionResult> Edit(ModyficationRoleViewModel model)
        {
            IdentityResult result;
            var userManager = HttpContext.GetOwinContext().GetUserManager<AppUserManager>();

            if (ModelState.IsValid)
            {
                foreach (string userId in model.UsersToAds ?? new string[] { })
                {
                    result = await userManager.AddToRoleAsync(userId, model.RoleName);

                    if (!result.Succeeded)
                        return View("Error", result.Errors);
                }

                foreach (string userId in model.UsersToRemoves ?? new string[] { })
                {
                    result = await userManager.RemoveFromRoleAsync(userId, model.RoleName);

                    if (!result.Succeeded)
                        return View("Error", result.Errors);
                }

                return RedirectToAction("Index");
            }
            else
                return View("Error", new string[] { "Brak roli!" });
        }

        public ActionResult RemoveRole()
        {
            return View();
        }
    }
}