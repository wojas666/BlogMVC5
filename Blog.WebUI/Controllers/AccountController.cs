using Blog.Domain.Concrete;
using Blog.Domain.Entity;
using Blog.WebUI.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog.WebUI.Controllers
{
    public class AccountController : Controller
    {
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View();
        }

        public AccountController()
        {

        }

        public ViewResult CreateNewAccount()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateNewAccount(RegisterViewModel model, String returnUrl)
        {
            if (ModelState.IsValid)
            {
                var userManager = HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
                var roleManager = HttpContext.GetOwinContext().GetUserManager<RoleManager<AppRole>>();

                User user = new Domain.Entity.User()
                {
                    UserName = model.Login,
                    Email = model.EMail,
                    UserProfile = new UserProfile()
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Age = model.Age,
                    }
                };
                
                IdentityResult result = userManager.Create(user, model.Password);
                userManager.AddToRole(userManager.FindByName(user.UserName).Id, "User");

                if (result.Succeeded)
                    return Redirect(returnUrl ?? Url.Action("SignIn", "Access"));
                
            }

            ModelState.AddModelError("", "Rejestracja nie powiodła się!");
            return View();
        }

        [HttpPost]
        public ActionResult AccountSettings()
        {
            return View();
        }
    }
}